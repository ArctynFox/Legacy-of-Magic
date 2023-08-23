using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Parameters : MonoBehaviour
{
    public int lifeSetting = 3;
    public int lives = 3;
    GameObject[] hearts = new GameObject[5];
    public int bombSetting = 3;
    public int bombs = 3;
    GameObject[] spells = new GameObject[5];
    public int continues = 5;
    public int stage = 0;
    string stageName = "title";
    public string sceneName;
    public int score = 0;
    public bool isBoss = false;
    public bool isBossDead = false;
    public Image fadeTransition;
    public GameObject continueScreen;
    public GameObject continueFirstButton;
    public GameObject gameOverScreen;
    public GameObject gameOverFirstButton;
    public PlayerController playerCont;
    public GameObject bossDialoguePre;
    public GameObject canvas;

    
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("gameParams").Length > 1)
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = GameObject.Find("Life " + i);
            spells[i] = GameObject.Find("Bomb " + i);
        }
        DontDestroyOnLoad(gameObject);
        
        //playerCont = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void setLives()
    {
        lives = lifeSetting - 1;
    }

    public void setBombs()
    {
        bombs = bombSetting - 1;
    }

    private void Update()
    {
        if(lives < 0 && stage > 0 && stage < 6 && (!continueScreen.activeSelf && !gameOverScreen.activeSelf))
        {
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 0;
            if (continues > 0)
            {
                continueScreen.SetActive(true);
                GameObject.Find("RemainingContinues").GetComponent<Text>().text = continues + " continues remaining";
                EventSystem.current.SetSelectedGameObject(continueFirstButton);
            } 
            else
            {
                gameOverScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
            }
        }
        if (hearts[0] != null)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i <= lives)
                {
                    hearts[i].SetActive(true);
                }
                else
                {
                    hearts[i].SetActive(false);
                }
                if (i <= bombs)
                {
                    spells[i].SetActive(true);
                }
                else
                {
                    spells[i].SetActive(false);
                }
            }
        }
    }

    public GameObject bossSpawned(GameObject preDialogue)
    {
        if (!isBoss)
        {
            isBoss = true;
            playerCont.enemyCardActive = true;
            bossDialoguePre = Instantiate(preDialogue, canvas.transform);
            return bossDialoguePre;
        }
        else
        {
            return GameObject.Find("Boss Dialogue Pre(Clone)");
        }

    }

    public void NextStage()
    {
        switch (stage)
        {
            case 5:
                sceneName = "Ending";
                stageName = "End";
                break;
            case 4:
                sceneName = "Stage 5";
                stageName = "Mage Association Research Tower";
                break;
            case 3:
                sceneName = "Stage 4";
                stageName = "Grand Magus Archives";
                break;
            case 2:
                sceneName = "Stage 3";
                stageName = "Volcanic Swamp";
                break;
            case 1:
                sceneName = "Stage 2";
                stageName = "Lake of Spirits";
                break;
            case 0:
                sceneName = "Stage 1";
                stageName = "Alresia Forest";
                break;
            default:
                sceneName = "Title Screen";
                stageName = "Title Screen";
                stage = 0;
                break;
        }
        StartCoroutine("StageSwitch");
    }

    IEnumerator StageSwitch()
    {
        float alpha = 0;
        while(alpha <= 1)
        {
            alpha += .02f;
            fadeTransition.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        isBoss = false;
        SceneManager.LoadScene(sceneName);
        stage++;
        yield return new WaitForSeconds(.25f);
        fadeTransition = GameObject.Find("FadeTransition").GetComponent<Image>();
        if (stage > 0 && stage < 6)
        {
            GameObject stageNameDisp = GameObject.Find("Stage Indicator YYK");
            stageNameDisp.GetComponent<Text>().text = stageName;
            stageNameDisp.transform.parent.gameObject.GetComponent<Text>().text = stageName;
            /*continueScreen = GameObject.Find("Continue Menu");
            continueFirstButton = GameObject.Find("Continue Button");
            gameOverScreen = GameObject.Find("Game Over Menu");
            gameOverFirstButton = GameObject.Find("Restart Button");*/
            playerCont = GameObject.Find("Player").GetComponent<PlayerController>();
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i] = GameObject.Find("Life " + i);
                spells[i] = GameObject.Find("Bomb " + i);
            }
        }
        while (alpha >= 0)
        {
            alpha -= .02f;
            fadeTransition.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
