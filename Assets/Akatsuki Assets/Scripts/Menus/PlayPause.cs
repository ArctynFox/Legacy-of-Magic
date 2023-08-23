using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayPause : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject continueMenu;
    public string menuSceneName = "Title Screen";
    public string restartSceneName = "Stage Scene";

    public GameObject firstSelected;

    Parameters param;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
        param.canvas = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else if(!continueMenu.activeSelf)
            {
                Pause(firstSelected);
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming game.");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    void Pause(GameObject selected)
    {
        Debug.Log("Pausing Game.");
        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selected);
    }

    public void continueButton()
    {
        param.score = 0;
        param.continues--;
        Debug.Log("Using a continue. " + param.continues + " continues remaining.");
        param.setLives();
        continueMenu.SetActive(false); 
        Time.timeScale = 1;
    }

    public void Retry()
    {
        Debug.Log("Restarting from stage 1.");
        param.score = 0;
        param.setLives();
        param.setBombs();
        param.continues = 5;
        param.stage = 0;
        param.NextStage();
    }

    public void ReturnToMenu()
    {
        Debug.Log("Returning to Main Menu.");
        param.stage = -1;
        param.NextStage();
    }
}
