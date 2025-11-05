using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour
{
    public GameObject gameParameters;
    Parameters param;

    public GameObject mainMenu;
    public GameObject options;
    public GameObject manual;

    public GameObject menuFirstButton, optionsFirstButton, optionsCloseButton, manualFirstButton, manualCloseButton;

    private void Start()
    {
        gameParameters = GameObject.Find("gameParameters");
        param = gameParameters.GetComponent<Parameters>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        
    }

    public void StartGame()
    {
        Debug.Log("Starting game.");
        param.setLives();
        param.setBombs();
        param.stage = 0;
        param.score = 0;
        param.NextStage();
    }

    public void Options()
    {
        Debug.Log("Opening Options.");
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void ReturnToMenu()
    {
        Debug.Log("Returning to menu.");
        EventSystem.current.SetSelectedGameObject(null);
        options.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }

    public void Manual()
    {
        Debug.Log("Opening manual.");
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        manual.SetActive(true);
        EventSystem.current.SetSelectedGameObject(manualFirstButton);
    }

    public void ManualReturnToMenu()
    {
        Debug.Log("Returning to menu.");
        EventSystem.current.SetSelectedGameObject(null);
        manual.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(manualCloseButton);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }
}
