using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//ポーズメニュー機能

public class PlayPause : MonoBehaviour
{
    //ポーズボタンAction
    InputAction menu;

    //ポーズメニュー参照
    public GameObject pauseMenu;
    //コンティニューメニュー参照
    public GameObject continueMenu;
    //ゲームオーバーメニュー参照
    public GameObject gameOverMenu;

    //メニューが表示されたら、最初にホバー状態にあるボタン参照
    public GameObject firstSelected;

    void Start()
    {
        Time.timeScale = 1;
        Parameters.singleton.canvas = gameObject;
    }

    //メニューボタン機能を設定、有効にする
    private void OnEnable()
    {
        menu = InputManager.inputActions.Default.Menu;
        menu.performed += OnMenuPressed;
        menu.Enable();
    }

    //メニューボタンを無効にする
    private void OnDisable()
    {
        menu.performed -= OnMenuPressed;
        menu.Disable();
    }

    //メニューボタンが押されたら、ポーズメニューを表示または非表示にする
    void OnMenuPressed(InputAction.CallbackContext callbackContext)
    {
        if (pauseMenu.activeSelf)
        {
            Resume();
        }
        else if (!continueMenu.activeSelf && !gameOverMenu.activeSelf)
        {
            Pause(firstSelected);
        }
    }

    //ポーズメニューを閉じてゲームを再開
    public void Resume()
    {
        Debug.Log("Resuming game.");
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
        PlayerController.singleton.EnableAllInputActions();
        pauseMenu.SetActive(false);
    }

    //ポーズメニューを表示
    void Pause(GameObject selected)
    {
        Debug.Log("Pausing Game.");
        PlayerController.singleton.DisableAllInputActions();
        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selected);
    }

    //コンティニューのコンティニューボタンが押されたらこれが起動
    public void continueButton()
    {
        //スコアをリセット
        Parameters.singleton.score = 0;
        //残っているコンティニュー数を減らす
        Parameters.singleton.continues--;
        Debug.Log("Using a continue. " + Parameters.singleton.continues + " continues remaining.");

        //ライフ数をリセット
        Parameters.singleton.setCurrentLives();
        continueMenu.SetActive(false);

        //ゲームを再開
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
        PlayerController.singleton.EnableAllInputActions();
    }

    //ステージ1から再開始
    public void Retry()
    {
        Debug.Log("Restarting from stage 1.");
        Parameters.singleton.score = 0;
        Parameters.singleton.setCurrentLives();
        Parameters.singleton.setBombs();
        Parameters.singleton.continues = 5;
        Parameters.singleton.stageID = 0;
        Parameters.singleton.NextStage();
    }

    //タイトル画面に戻る
    public void ReturnToMenu()
    {
        Debug.Log("Returning to Main Menu.");
        Parameters.singleton.stageID = -1;
        Parameters.singleton.NextStage();
    }
}
