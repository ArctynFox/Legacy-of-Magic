using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//ボスの形態やHPやダイアログや衝突などを整理するスクリプト

public class BossController : MonoBehaviour
{
    //形態制御
    [Header("Phase Management")]
    public int phaseCount = 3;
    public int phase = 0;
    public float[] phaseTimeLimits;
    float currentTimeLimit;

    //HP制御
    [Header("HP Management")]
    public int maxHealth = 40;
    public int health;
    public int[] phaseHealths;
    private bool isAlreadyDead = false;
    bool invulnerable;

    //必要なプレハブ
    [Header("Prefabs")]
    public GameObject bomb;
    public GameObject deathSprite;
    public GameObject[] emittersPrefabs;
    public GameObject healthbarObject;
    public GameObject timer;
    public GameObject bossDialoguePre;
    public GameObject bossDialoguePost;
    public GameObject enemyDeathAnim;
    public GameObject bulletDeathAnim;

    //必要なオブジェクト参照
    [Header("Runtime Object References")]
    public Slider healthbar;
    public Text timerText;
    public GameObject[] emitters;
    public GameObject canvas;
    DialogueManager preDialogue;

    //ダイアログが終わったかどうか
    public bool dialogueOver = false;

    void Start()
    {
        //ボスがスポーンされたら全ての弾と敵を削除
        //when the boss spawns, all enemies and bullets need to die
        DestroyAllBullets();
        GameObject[] destroyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in destroyList)//destroy all enemies
        {
            Instantiate(enemyDeathAnim, enemy.transform).transform.SetParent(null);
            Destroy(enemy);
        }

        //boss setup
        //形態数を設定
        phaseCount = phaseHealths.Length;//failsafe
        //現在と最大限のHPを設定
        health = phaseHealths[0];//the next few lines are all to set parameters and initiate pre-fight dialogue
        maxHealth = phaseHealths[0];//set the max health for the first phase
        //ボス戦前のダイアログを開始
        bossDialoguePre = Parameters.singleton.instantiatePreBossDialogue(bossDialoguePre);//start the pre-boss-fight dialogue
        //必要なデータをダイアログスクリプトに渡す
        preDialogue = bossDialoguePre.GetComponentInChildren<DialogueManager>();
        preDialogue.boss = gameObject;//give the dialogue a reference to the boss object
        preDialogue.emitter0 = emittersPrefabs[0];//informs the dialogue of various objects necessary to start the battle
        preDialogue.healthbar = healthbarObject;
        preDialogue.timer = timer;
        preDialogue.firstTimeLimit = phaseTimeLimits[0];
        //第一形態の弾スポナーを開始
        emitters = new GameObject[emittersPrefabs.Length];//prepares emitters for boss phases
        //第一形態の時間制限を設定
        currentTimeLimit = phaseTimeLimits[0];
        //後でダイアログをスポーンするためにUIキャンバスの参照を取得
        canvas = GameObject.Find("Canvas");//get a reference to the UI canvas for spawning the dialogue objects
    }

    void FixedUpdate()
    {
        if (healthbar != null)
        {
            healthbar.value = health;
        }

        if (!dialogueOver)
        {
            return;
        }

        currentTimeLimit -= Time.fixedDeltaTime;
        timerText.text = currentTimeLimit.ToString();

        //現在の形態の時間制限はまだ切れてない場合、他に何もしない
        //if phase time limit is not exhausted, do nothing else
        if (currentTimeLimit > 0)
        {
            return;
        }

        //現在の形態の時間制限が切れた場合、次の形態に変化
        //if phase time limit is exhausted, proceed to next phase
        if (phase < emittersPrefabs.Length - 1)
        {
            phase++;
            NextPhase();
        }
        //でなければ、ボスが死ぬ
        else //if no more phases exist, the boss fight ends
        {
            if (!isAlreadyDead)//prevents boss from "dying" multiple times in edge cases
            {
                OnBossDeath();
            }
        }
    }

    //ボスが何かにぶつかったらこれが起動
    void OnTriggerEnter2D(Collider2D other)
    {
        //無敵ではないなら
        if (!invulnerable)
        {
            //プレイヤーが発射した弾とぶつかったら
            if (other.gameObject.CompareTag("PlayerBullet"))//only takes damage/changes phase if hit by the player's bullets
            {
                //現在HPを削減
                health--;//decreases health by 1 per hit

                //HPがゼロになったら
                if (health < 1)
                {
                    //また次の形態があれば形態を変化
                    phase++;//goes to next phase if health reaches 0
                    if (phase < phaseCount)
                    {
                        //スコアに足す
                        Parameters.singleton.score += 10000;//adds to score if phase change was initiated by one of the player's bullets
                        NextPhase();
                    }
                    //なければ死ぬ
                    else
                    {
                        if (!isAlreadyDead)//another measure to make sure the boss doesn't get bugged
                        {
                            //スコアに足す
                            Parameters.singleton.score += 50000;//adds to score if boss was beaten by player
                            OnBossDeath();
                        }
                    }
                }
                Destroy(other.gameObject);//destroys the bullet that hit the boss
            }
        }
    }

    //ボスが死んだらこれを起動
    void OnBossDeath()
    {
        //全ての弾を消す
        DestroyAllBullets();
        //死アニメーションを開始
        Instantiate(deathSprite, transform).transform.SetParent(null);//death explosion for boss
        //プレイヤーの発射やスペルカードを無効にする
        PlayerController.singleton.DisableDestructiveInputActions();//prevents player from firing to make sure it doesn't initiate the post-fight dialogue multiple times
        //ボス戦後のダイアログを開始
        Instantiate(bossDialoguePost, canvas.transform);//instantiates the post-fight dialogue
        //ボスの発射を停止
        emitters[emitters.Length - 1].SetActive(false);//sets the final bullet emitter to inactive
        //フェイルセーフブール値：ダイアログが複数回開始されないようにする
        dialogueOver = false;//another measure to make sure the player can't initiate dialogue a second time
        //タイマーを削除
        Destroy(timer);//remove boss phase countdown timer
        //HPゲージを削除
        Destroy(GameObject.FindGameObjectWithTag("HealthBar"));//remove boss health bar
    }

    //全ての弾を消す
    void DestroyAllBullets()
    {
        //プレイヤーが発射した弾を消す
        //destroy all player bullets
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }

        //敵が発射した弾を消す
        //destroy all enemy bullets
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }
    }

    //次の形態に変化
    void NextPhase()//changes properties and emitters to next boss phase
    {
        //スペルカードをスポーン
        Instantiate(bomb, transform);//creates spellcard, gets rid of all bullets for 2 seconds
        //現在の形態の弾スポナーを停止
        emitters[phase - 1].SetActive(false);//turns off the previous emitter
        //次の形態の弾スポナーをスポーン
        emitters[phase] = Instantiate(emittersPrefabs[phase], transform);//creates the next phase's emitter
        //形態の時間制限を設定
        currentTimeLimit = phaseTimeLimits[phase];//sets the phase time limit
        //形態のHP最大限を設定
        healthbar.maxValue = phaseHealths[phase];//sets the phase max health
        //現在HPを最大限に設定
        health = phaseHealths[phase];//resets the current health to the phase max
        //全ての弾を消す
        DestroyAllBullets();
        //一旦無敵になる
        StartCoroutine(InvulnerabilityTimer());
    }

    //２秒無敵
    IEnumerator InvulnerabilityTimer()//a timer for how long the boss is invulnerable upon using a spell
    {
        invulnerable = true;
        yield return new WaitForSeconds(2f);
        invulnerable = false;
    }
} 