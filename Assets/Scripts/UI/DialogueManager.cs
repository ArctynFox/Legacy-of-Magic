using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//ダイアログの全ての機能を管理するスクリプト

public class DialogueManager : MonoBehaviour
{
    DialogueManager singleton;

    InputAction select;

    //ダイアログのテキストボックス
    public Text currentDialogue;
    //ボスの第一形態の弾スポナーの参照
    [Header("Battle Start Setup")]
    //ボスの第一形態の弾スポナー
    public GameObject emitter0;
    //ボスのHPゲージプレハブ
    public GameObject healthbar;
    //ボス参照
    public GameObject boss;
    //形態タイマープレハブ
    public GameObject timer;
    //再生するボス線BGM
    public GameObject bossBGM;
    //第一形態の時間制限
    public float firstTimeLimit;

    [Header("Dialogue Info")]
    public string[] dialogueList = new string[20];
    int lineNumber = 0;
    //ボス戦後のダイアログかどうか
    public bool isPostBattle = false;
    //ダイアログがもう終わったかどうか
    public bool isDialogueEnded = false;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }

        //プレイヤーの発射とスペルカード機能を無効にする
        PlayerController.singleton.DisableDestructiveInputActions();
    }

    //入力機能を有効にする
    void Start()
    {
        DestroyDuplicateDialoguesIfPresent();
        currentDialogue.text = dialogueList[lineNumber];
        select = InputManager.inputActions.UI.Select;
        select.performed += OnSelectPressed;
        select.Enable();
    }

    //複数の弾が同時にボスを衝突したら、複数のダイアログを生成する可能性があるので
    //複数のダイアログが存在していたら、一つ除いて削除
    void DestroyDuplicateDialoguesIfPresent()
    {
        GameObject[] dialogueObjects = GameObject.FindGameObjectsWithTag("Dialogue");
        if(dialogueObjects.Length > 1)
        {
            Debug.Log("Duplicate dialogues exist. Destroying extras.");
            for(int i = 1; i < dialogueObjects.Length; i++)
            {
                try
                {
                    Destroy(dialogueObjects[i]);
                } catch(NullReferenceException e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    private void OnDisable()
    {
        select.performed -= OnSelectPressed;
        select.Disable();
    }

    //プレイヤーが発射ボタンを押したらこれが起動
    void OnSelectPressed(InputAction.CallbackContext callbackContext)
    {
        //ダイアログはもう終わったら何もしない
        if (isDialogueEnded)
        {
            return;
        }

        //ダイアログを進む
        lineNumber++;
        if (lineNumber < dialogueList.Length)
        {
            currentDialogue.text = dialogueList[lineNumber];
        }
        //ダイアログが終わったら
        else
        {
            isDialogueEnded = true;
            //ボス戦後ダイアログだったら次のステージに進む
            if (isPostBattle)
            {
                Parameters.singleton.NextStage();
            }
            //でなければボス戦を開始
            else
            {
                StartBossFight();
            }

            //ダイアログのUIオブジェクトを削除
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {
        DestroyDuplicateDialoguesIfPresent();
    }

    //ボス戦開始の設定
    void StartBossFight()
    {
        //プレイヤーの発射とスペルカード入力を有効にする
        PlayerController.singleton.EnableDestructiveInputActions();

        //HPゲージをスポーンし、設定
        healthbar = Instantiate(healthbar, GameObject.Find("Canvas").transform);
        healthbar.GetComponent<GetBossHealth>().boss = boss;
        healthbar.GetComponent<Slider>().maxValue = boss.GetComponent<BossController>().phaseHealths[0];
        boss.GetComponent<BossController>().healthbar = healthbar.GetComponent<Slider>();

        //ボスの第一形態の弾スポナーをスポーン
        boss.GetComponent<BossController>().emitters[0] = Instantiate(emitter0, boss.transform);

        //形態タイマーをスポーンし、設定
        timer = Instantiate(timer, GameObject.Find("Canvas").transform);
        boss.GetComponent<BossController>().timer = timer;
        boss.GetComponent<BossController>().timerText = timer.GetComponent<Text>();
        timer.GetComponent<Text>().text = firstTimeLimit.ToString();

        //ボスの無敵状態を解除
        boss.GetComponent<BossController>().dialogueOver = true;

        //ボス戦のBGMを再生
        Instantiate(bossBGM);
    }
}
