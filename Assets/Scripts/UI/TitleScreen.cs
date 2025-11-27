using UnityEngine;
using UnityEngine.EventSystems;

//タイトル画面の全ての機能を管理するスクリプト

public class TitleScreen : MonoBehaviour
{
    public GameObject gameParameters;

    public GameObject mainMenu;
    public GameObject options;
    public GameObject manual;

    public GameObject menuFirstButton, optionsFirstButton, optionsCloseButton, manualFirstButton, manualCloseButton;

    //最初にホバー状態にあるボタンを設定
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }

    //ゲームを開始
    public void StartGame()
    {
        Debug.Log("Starting game.");
        Parameters.singleton.setCurrentLives();
        Parameters.singleton.setCurrentBombs();
        Parameters.singleton.stageID = 0;
        Parameters.singleton.score = 0;
        Parameters.singleton.NextStage();
    }

    //設定画面を開く
    public void Options()
    {
        Debug.Log("Opening Options.");
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    //タイトル画面に戻る
    public void ReturnToMenu()
    {
        Debug.Log("Returning to menu.");
        EventSystem.current.SetSelectedGameObject(null);
        options.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }

    //マニュアルを開く
    public void Manual()
    {
        Debug.Log("Opening manual.");
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        manual.SetActive(true);
        EventSystem.current.SetSelectedGameObject(manualFirstButton);
    }

    //タイトル画面に戻る
    public void ManualReturnToMenu()
    {
        Debug.Log("Returning to menu.");
        EventSystem.current.SetSelectedGameObject(null);
        manual.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(manualCloseButton);
    }

    //ゲームを終了
    public void QuitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }
}
