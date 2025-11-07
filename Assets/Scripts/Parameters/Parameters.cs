using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Parameters : MonoBehaviour
{
    public static Parameters singleton;
    public int lifeSetting = 3;
    public int lives = 3;
    GameObject[] hearts = new GameObject[5];
    public int bombSetting = 3;
    public int bombs = 3;
    GameObject[] spells = new GameObject[5];
    public int continues = 5;
    public int stage = 0;
    string stageName = "Title Screen";
    public string sceneName = "Title Screen";
    public int score = 0;
    public bool isBoss = false;
    public bool stageSwitchInitiated = false;
    public Image fadeTransition;
    public GameObject continueScreen;
    public GameObject continueFirstButton;
    public GameObject gameOverScreen;
    public GameObject gameOverFirstButton;
    public GameObject bossDialoguePre;
    public GameObject canvas;

    
    private void Awake()
    {
        //destroy self if the parameters script already exists in the scene
        if(singleton != null)
        {
            Destroy(gameObject);
        } else
        {
            singleton = this;
        }

        Cursor.visible = false;
#if UNITY_EDITOR
        Cursor.visible = true;
#endif

        //DEBUG: this only does anything if starting the game from a scene other than the title screen
        //get references to the bomb and heart graphics and updates their display based on the current life and bomb count
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = GameObject.Find("Life " + i);
            spells[i] = GameObject.Find("Bomb " + i);
        }
        if (hearts[0] != null && spells[0] != null)
        {
            updateHeartAndBombDisplay();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if(singleton == this)
        {
            singleton = null;
        }
    }

    public void setLifeGraphicArray(GameObject[] newArray)
    {
        hearts = newArray;
    }
    
    public void setBombGraphicArray(GameObject[] newArray)
    {
        spells = newArray;
    }

    public void setLives()
    {
        lives = lifeSetting - 1;
        updateHeartAndBombDisplay();
    }

    public void setBombs()
    {
        bombs = bombSetting - 1;
        updateBombDisplay();
    }

    bool outOfLives()
    {
        return lives < 0 && stage > 0 && stage < 6;
    }

    bool menusAreNotVisible()
    {
        return !continueScreen.activeSelf && !gameOverScreen.activeSelf;
    }

    void displayContinueScreen()
    {
        continueScreen.SetActive(true);
        GameObject.Find("RemainingContinues").GetComponent<Text>().text = continues + " continues remaining";
        EventSystem.current.SetSelectedGameObject(continueFirstButton);
    }

    void displayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
    }

    public void updateHeartAndBombDisplay()
    {
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
            }
        }
        updateBombDisplay();
    }
    
    public void updateBombDisplay()
    {
        if (spells[0] != null)
        {
            for (int i = 0; i < spells.Length; i++)
            {
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

    public void playerTookDamage()
    {
        lives--;

        //if lives have run out, display continue or game over screen based on number of remaining continues
        if(outOfLives() && menusAreNotVisible())
        {
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 0;
            if (continues > 0)
            {
                displayContinueScreen();
            } 
            else
            {
                displayGameOverScreen();
            }
        }
       updateHeartAndBombDisplay(); 
    }

    public GameObject instantiatePreBossDialogue(GameObject preDialogue)
    {
        if (!isBoss)
        {
            isBoss = true;
            PlayerController.singleton.DisableDestructiveInputActions();
            bossDialoguePre = Instantiate(preDialogue, canvas.transform);
            return bossDialoguePre;
        }
        else
        {
            return GameObject.Find("Boss Dialogue Pre(Clone)");
        }

    }

    //check the stage variable for which scene to transition to and initiate the scene transition
    public void NextStage()
    {
        if(stageSwitchInitiated) { return; }
        stageSwitchInitiated = true;

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
                stage = -1;
                break;
        }
        StartCoroutine(StageSwitch());
    }

    //load the next scene
    IEnumerator StageSwitch()
    {
        //pre-scene change setup------------------------------
        //fade the screen to black
        float alpha = 0;
        while(alpha <= 1)
        {
            alpha += .02f;
            fadeTransition.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        //disable boss state
        isBoss = false;
        //pre-scene change setup END--------------------------

        //load the scene and increment the stage counter
        SceneManager.LoadScene(sceneName);
        stage++;

        //post-scene change setup-----------------------------
        yield return new WaitForSeconds(.25f);
        //if current scene is a gameplay scene
        if (stage > 0 && stage < 6)
        {
            //update UI elements for current gameplay info
            GameObject stageNameDisp = GameObject.Find("Stage Indicator YYK");
            stageNameDisp.GetComponent<Text>().text = stageName;
            stageNameDisp.transform.parent.gameObject.GetComponent<Text>().text = stageName;
            updateHeartAndBombDisplay();
        }

        fadeTransition = GameObject.Find("FadeTransition").GetComponent<Image>();

        //fade the screen from black
        while (alpha >= 0)
        {
            alpha -= .02f;
            fadeTransition.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        stageSwitchInitiated = false;
        //post-scene change setup END------------------------
    }
}
