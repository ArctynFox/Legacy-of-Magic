using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayPause : MonoBehaviour
{
    InputAction menu;

    public GameObject pauseMenu;
    public GameObject continueMenu;
    public GameObject gameOverMenu;
    public string menuSceneName = "Title Screen";
    public string restartSceneName = "Stage Scene";

    public GameObject firstSelected;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Parameters.singleton.canvas = gameObject;
    }

    private void OnEnable()
    {
        menu = InputManager.inputActions.Default.Menu;
        menu.performed += OnMenuPressed;
        menu.Enable();
    }

    private void OnDisable()
    {
        menu.performed -= OnMenuPressed;
        menu.Disable();
    }

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

    public void Resume()
    {
        Debug.Log("Resuming game.");
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
        PlayerController.singleton.EnableAllInputActions();
        pauseMenu.SetActive(false);
    }

    void Pause(GameObject selected)
    {
        Debug.Log("Pausing Game.");
        PlayerController.singleton.DisableAllInputActions();
        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selected);
    }

    public void continueButton()
    {
        Parameters.singleton.score = 0;
        Parameters.singleton.continues--;
        Debug.Log("Using a continue. " + Parameters.singleton.continues + " continues remaining.");
        Parameters.singleton.setLives();
        continueMenu.SetActive(false); 
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
        PlayerController.singleton.EnableAllInputActions();
    }

    public void Retry()
    {
        Debug.Log("Restarting from stage 1.");
        Parameters.singleton.score = 0;
        Parameters.singleton.setLives();
        Parameters.singleton.setBombs();
        Parameters.singleton.continues = 5;
        Parameters.singleton.stage = 0;
        Parameters.singleton.NextStage();
    }

    public void ReturnToMenu()
    {
        Debug.Log("Returning to Main Menu.");
        Parameters.singleton.stage = -1;
        Parameters.singleton.NextStage();
    }
}
