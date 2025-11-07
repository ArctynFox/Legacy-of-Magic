using UnityEngine;
using UnityEngine.UI;

public class AdvanceDialogue : MonoBehaviour
{
    AdvanceDialogue singleton;

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
    }

    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Dialogue").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Dialogue")[1]);
        }
        currentDialogue.text = dialogueList[lineNumber];
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDialogueEnded)
        {
            lineNumber++;
            if (lineNumber < dialogueList.Length)
            {
                currentDialogue.text = dialogueList[lineNumber];
            }
            else
            {
                isDialogueEnded = true;
                Debug.Log("firing is disabled: " + PlayerController.singleton.firingDisabled);
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
    }

    void StartBossFight()
    {
        PlayerController.singleton.firingDisabled = false;
        (healthbar = Instantiate(healthbar, GameObject.Find("Canvas").transform)).GetComponent<GetBossHealth>().boss = boss;
        //boss.GetComponent<BossCollision>().phase = 1;
        boss.GetComponent<BossCollision>().healthbar = healthbar.GetComponent<Slider>();
        healthbar.GetComponent<Slider>().maxValue = boss.GetComponent<BossCollision>().phaseHealths[0];
        boss.GetComponent<BossCollision>().emitters[0] = Instantiate(emitter0, boss.transform);
        timer = Instantiate(timer, GameObject.Find("Canvas").transform);
        boss.GetComponent<BossCollision>().timer = timer;
        boss.GetComponent<BossCollision>().timerText = timer.GetComponent<Text>();
        timer.GetComponent<Text>().text = firstTimeLimit.ToString();
        boss.GetComponent<BossCollision>().dialogueOver = true;
        Instantiate(bossBGM);
        Debug.Log("firing is disabled: " + PlayerController.singleton.firingDisabled);
    }
}
