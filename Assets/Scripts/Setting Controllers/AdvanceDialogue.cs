using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceDialogue : MonoBehaviour
{
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

    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Dialogue").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Dialogue")[1]);
        }
        currentDialogue.text = dialogueList[lineNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            lineNumber++;
            if (lineNumber < dialogueList.Length)
            {
                currentDialogue.text = dialogueList[lineNumber];
            }
            else
            {
                if (isPostBattle)
                {
                    GameObject.Find("gameParameters").GetComponent<Parameters>().NextStage();
                } 
                else
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().enemyCardActive = false;
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
                }
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
