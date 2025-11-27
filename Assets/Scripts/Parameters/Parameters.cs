using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//ゲームの状況やUIを管理するスクリプト

public class Parameters : MonoBehaviour
{
    //シングルトン
    public static Parameters singleton;

    //ライフに関する画像や数値
    [Header("Life Data")]
    [Tooltip("Number of lives to spawn with per continue.")]
    public int lifeSetting = 3;
    [Tooltip("Current number of lives.")]
    public int lives = 3;
    [Tooltip("Life UI object references.")]
    GameObject[] hearts = new GameObject[5];

    //スペルカードに関する画像や数値
    [Header("Spellcard Data")]
    [Tooltip("Number of spellcards to spawn with per life.")]
    public int bombSetting = 3;
    [Tooltip("Current number of spellcards.")]
    public int bombs = 3;
    [Tooltip("Spellcard UI object references.")]
    GameObject[] spells = new GameObject[5];

    //残っているコンティニュー数
    [Header("Continues Data")]
    [Tooltip("Number of continues remaining.")]
    public int continues = 5;

    //現在ステージID
    [Header("Stage Data")]
    [Tooltip("Current stage ID.")]
    public int stageID = -1;
    //UIに見えるステージ名
    [Tooltip("Stage name to display in the UI.")]
    string stageName = "Title Screen";
    //ステージのシーンのファイル名
    [Tooltip("Scene name for the stage.")]
    public string sceneName = "Title Screen";

    //ゲームの現在の状況に関する変数
    [Header("Game State Data")]
    //スコア
    [Tooltip("Current score.")]
    public int score = 0;
    //ステージのボスが現れたかどうか
    [Tooltip("True if the stage boss is active.")]
    public bool isBoss = false;
    //ステージ変更はもう起動されたかどうか
    [Tooltip("True if the stage is currently being switched to the next one.")]
    public bool stageSwitchInitiated = false;

    //UIオブジェクト参照
    [Header("UI Object References")]
    public Image fadeTransition;
    public GameObject continueScreen;
    public GameObject continueFirstButton;
    public GameObject gameOverScreen;
    public GameObject gameOverFirstButton;
    public GameObject bossDialoguePre;
    public GameObject canvas;

    
    private void Awake()
    {
        //このスクリプトはシーン内にもう存在しているなら自分を削除する
        //destroy self if the parameters script already exists in the scene
        if(singleton != null)
        {
            Destroy(gameObject);
        } else
        {
            singleton = this;
        }

        //カーソルを透明にする
        Cursor.visible = false;
#if UNITY_EDITOR
        //エディターだったら透明を解除
        Cursor.visible = true;
#endif

        //デバッグ用-------------------------
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
        //デバッグ終わり----------------------

        DontDestroyOnLoad(gameObject);
    }

    //削除の際、シングルトンをヌルに設定
    private void OnDestroy()
    {
        if(singleton == this)
        {
            singleton = null;
        }
    }

    //heartsを渡されたGameObject配列に設定
    public void setLifeGraphicArray(GameObject[] newArray)
    {
        hearts = newArray;
    }
    
    //spellsを渡されたGameObject配列に設定
    public void setBombGraphicArray(GameObject[] newArray)
    {
        spells = newArray;
    }

    //現在ライフ数を設定されたライフ最大限に設定
    public void setCurrentLives()
    {
        lives = lifeSetting - 1;
        updateHeartAndBombDisplay();
    }

    //現在スペルカード数を設定されたスペルカード最大限に設定
    public void setCurrentBombs()
    {
        bombs = bombSetting - 1;
        updateBombDisplay();
    }

    //ライフは残っていないかどうか
    bool outOfLives()
    {
        return lives < 0 && stageID > 0 && stageID < 6;
    }

    //メニューが見えるかどうか
    bool menusAreNotVisible()
    {
        return !continueScreen.activeSelf && !gameOverScreen.activeSelf;
    }

    //コンティニュー画面を現す
    void displayContinueScreen()
    {
        continueScreen.SetActive(true);
        GameObject.Find("RemainingContinues").GetComponent<Text>().text = continues + " continues remaining";
        EventSystem.current.SetSelectedGameObject(continueFirstButton);
    }

    //ゲームオーバー画面を現す
    void displayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
    }

    //ライフとスペルカードの表示を更新
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
    
    //スペルカードの表示を更新
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

    //プレイヤーにダメージが与えられたら、ごれが起動
    public void playerTookDamage()
    {
        lives--;

        //ライフが残っていなかったら、状況によってコンティニューかゲームオーバー画面を表示
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

    //ボス戦前のダイアログを開始
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

    //次のステージを確認し、シーンをロード
    //check the stage variable for which scene to transition to and initiate the scene transition
    public void NextStage()
    {
        //ロード中だったら何もしない
        if(stageSwitchInitiated) { return; }
        stageSwitchInitiated = true;

        switch (stageID)
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
                stageID = -1;
                break;
        }
        StartCoroutine(StageSwitch());
    }

    //次のシーンをロード
    //load the next scene
    IEnumerator StageSwitch()
    {
        //シーン変更の前の必要設定
        //pre-scene change setup------------------------------
        //フェードアウト
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

        //シーン変更
        //load the scene and increment the stage counter
        SceneManager.LoadScene(sceneName);
        stageID++;
        stageSwitchInitiated = false;

        //シーン変更の後の必要設定
        //post-scene change setup-----------------------------
        yield return new WaitForSeconds(.25f);
        //if current scene is a gameplay scene
        if (stageID > 0 && stageID < 6)
        {
            //ステージとBGM名とライフ画像などを変更
            //update UI elements for current gameplay info
            GameObject stageNameDisp = GameObject.Find("Stage Indicator YYK");
            stageNameDisp.GetComponent<Text>().text = stageName;
            stageNameDisp.transform.parent.gameObject.GetComponent<Text>().text = stageName;
            updateHeartAndBombDisplay();
        }

        //フェードイン
        fadeTransition = GameObject.Find("FadeTransition").GetComponent<Image>();

        //fade the screen from black
        while (alpha >= 0)
        {
            alpha -= .02f;
            fadeTransition.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        //post-scene change setup END------------------------
    }
}
