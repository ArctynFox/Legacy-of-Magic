using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AdvanceDialogue : MonoBehaviour
{
    AdvanceDialogue singleton;

    InputAction select;

    public Text currentDialogue;
    public GameObject emitter0;
    public GameObject healthbar;
    public GameObject boss;
    public GameObject timer;
    public float firstTimeLimit;
    public GameObject bossBGM;

    public string[] dialogueList = new string[20];
    int lineNumber = 0;
    public bool isPostBattle = false;
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

        PlayerController.singleton.DisableDestructiveInputActions();
    }

    void Start()
    {
        DestroyDuplicateDialoguesIfPresent();
        currentDialogue.text = dialogueList[lineNumber];
        select = InputManager.inputActions.UI.Select;
        select.performed += OnSelectPressed;
        select.Enable();
    }

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

    void OnSelectPressed(InputAction.CallbackContext callbackContext)
    {
        if (isDialogueEnded)
        {
            return;
        }

        lineNumber++;
        if (lineNumber < dialogueList.Length)
        {
            currentDialogue.text = dialogueList[lineNumber];
        }
        else
        {
            isDialogueEnded = true;
            if (isPostBattle)
            {
                Parameters.singleton.NextStage();
            }
            else
            {
                StartBossFight();
            }
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {
        DestroyDuplicateDialoguesIfPresent();
    }

    void StartBossFight()
    {
        PlayerController.singleton.EnableDestructiveInputActions();
        (healthbar = Instantiate(healthbar, GameObject.Find("Canvas").transform)).GetComponent<GetBossHealth>().boss = boss;
        boss.GetComponent<BossController>().healthbar = healthbar.GetComponent<Slider>();
        healthbar.GetComponent<Slider>().maxValue = boss.GetComponent<BossController>().phaseHealths[0];
        boss.GetComponent<BossController>().emitters[0] = Instantiate(emitter0, boss.transform);
        timer = Instantiate(timer, GameObject.Find("Canvas").transform);
        boss.GetComponent<BossController>().timer = timer;
        boss.GetComponent<BossController>().timerText = timer.GetComponent<Text>();
        timer.GetComponent<Text>().text = firstTimeLimit.ToString();
        boss.GetComponent<BossController>().dialogueOver = true;
        Instantiate(bossBGM);
    }
}
