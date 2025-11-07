using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossCollision : MonoBehaviour
{
    public int healthBars = 3;
    public int phase = 0;
    public int maxHealth = 40;
    public int health;
    public GameObject bomb;
    public GameObject deathSprite;
    public GameObject[] emittersPrefabs;
    public GameObject[] emitters;
    public GameObject healthbarObject;
    public Slider healthbar;
    public int[] phaseHealths;
    public GameObject timer;
    public Text timerText;
    public float[] phaseTimeLimits;
    bool invulnerable;
    public GameObject bossDialoguePre;
    public GameObject bossDialoguePost;
    AdvanceDialogue preDialogue;
    private bool isAlreadyDead = false;
    public GameObject canvas;
    public GameObject enemyDeathAnim;
    public GameObject bulletDeathAnim;
    float currentTimeLimit;
    public bool dialogueOver = false;

    void Start()
    {
        //when the boss spawns, all enemies and bullets need to die
        DestroyAllBullets();
        GameObject[] destroyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in destroyList)//destroy all enemies
        {
            Instantiate(enemyDeathAnim, enemy.transform).transform.SetParent(null);
            Destroy(enemy);
        }

        //boss setup
        healthBars = phaseHealths.Length;//failsafe
        //healthbarObject = Instantiate(healthbarObject, GameObject.Find("Canvas").transform);
        //healthbar = GameObject.Find("Health Bar").GetComponent<Slider>();
        //param.bossDialoguePre = bossDialoguePre;
        health = phaseHealths[0];//the next few lines are all to set parameters and initiate pre-fight dialogue
        maxHealth = phaseHealths[0];//set the max health for the first phase
        bossDialoguePre = Parameters.singleton.instantiatePreBossDialogue(bossDialoguePre);//start the pre-boss-fight dialogue
        preDialogue = bossDialoguePre.GetComponentInChildren<AdvanceDialogue>();
        preDialogue.boss = gameObject;//give the dialogue a reference to the boss object
        emitters = new GameObject[emittersPrefabs.Length];//prepares emitters for boss phases
        preDialogue.emitter0 = emittersPrefabs[0];//informs the dialogue of various objects necessary to start the battle
        preDialogue.healthbar = healthbarObject;
        preDialogue.timer = timer;
        preDialogue.firstTimeLimit = phaseTimeLimits[0];
        canvas = GameObject.Find("Canvas");//get a reference to the UI canvas for spawning the dialogue objects
        currentTimeLimit = phaseTimeLimits[0];
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

        //if phase time limit is not exhausted, do nothing else
        if (currentTimeLimit > 0)
        {
            return;
        }

        //if phase time limit is exhausted, proceed to next phase
        if (phase < emittersPrefabs.Length - 1)
        {
            phase++;
            NextPhase();
        }
        else //if no more phases exist, the boss fight ends
        {
            if (!isAlreadyDead)//prevents boss from "dying" multiple times in edge cases
            {
                OnBossDeath();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (invulnerable)//anther measure in place to make sure the boss doesn't break - this probably does the exact same as the previous if-statement but inverted
        {
            return;
        }

        invulnerable = true;
        if (other.gameObject.tag == "PlayerBullet")//only takes damage/changes phase if hit by the player's bullets
        {
            health--;//decreases health by 1 per hit
            if (health < 1)
            {
                phase++;//goes to next phase if health reaches 0
                if (phase < healthBars)
                {
                    Parameters.singleton.score += 10000;//adds to score if phase change was initiated by one of the player's bullets
                    NextPhase();
                }
                else
                {
                    if (!isAlreadyDead)//another measure to make sure the boss doesn't get bugged
                    {
                        Parameters.singleton.score += 50000;//adds to score if boss was beaten by player
                        OnBossDeath();
                    }
                }
            }
            Destroy(other.gameObject);//destroys the bullet that hit the boss
        }
        invulnerable = false;
    }

    void OnBossDeath()
    {
        DestroyAllBullets();
        Instantiate(deathSprite, transform).transform.SetParent(null);//death explosion for boss
        PlayerController.singleton.DisableDestructiveInputActions();//prevents player from firing to make sure it doesn't initiate the post-fight dialogue multiple times
        //PlayerController.singleton.isPostBoss = true;
        Instantiate(bossDialoguePost, canvas.transform);//instantiates the post-fight dialogue
        emitters[emitters.Length - 1].SetActive(false);//sets the final bullet emitter to inactive
        dialogueOver = false;//another measure to make sure the player can't initiate dialogue a second time
        Destroy(timer);//remove boss phase countdown timer
        Destroy(GameObject.FindGameObjectWithTag("HealthBar"));//remove boss health bar
    }

    void DestroyAllBullets()
    {
        //destroy all player bullets
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }

        //destroy all enemy bullets
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }
    }

    void NextPhase()//changes properties and emitters to next boss phase
    {
        Instantiate(bomb, transform);//creates spellcard, gets rid of all bullets for 2 seconds
        emitters[phase - 1].SetActive(false);//turns off the previous emitter
        emitters[phase] = Instantiate(emittersPrefabs[phase], transform);//creates the next phase's emitter
        currentTimeLimit = phaseTimeLimits[phase];//sets the phase time limit
        healthbar.maxValue = phaseHealths[phase];//sets the phase max health
        health = phaseHealths[phase];//resets the current health to the phase max
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))//gets rid of all player bullets
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }
        StartCoroutine(InvulnerabilityTimer());
    }

    IEnumerator InvulnerabilityTimer()//a timer for how long the boss is invulnerable upon using a spell
    {
        invulnerable = true;
        //PlayerController.singleton.DisableDestructiveInputActions();//makes player unable to shoot for the duration of the spell
        /*if (GameObject.Find("Player Emitter") != null)
        {
            GameObject.Find("Player Emitter").SetActive(false);//turns off the player's emitter while the spell occurs
        }*/
        yield return new WaitForSeconds(2f);
        //PlayerController.singleton.EnableDestructiveInputActions();//allows the player to shoot again
        invulnerable = false;
    }
} 