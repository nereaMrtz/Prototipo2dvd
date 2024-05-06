using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    //public GameObject pauseMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public bool isPaused;
    public bool pauseInput;
   // public GameObject UI;

    private ScreenWipe sw;
    private Scene currentScene;

    const string mainMenu = "mainMenu";

    public void OnPause(InputAction.CallbackContext context)
    {
        float aux = context.ReadValue<float>();
        if (aux != 0f)
        {
            pauseInput = true;
        }
        else { pauseInput = false; }
    }

    void Start()
    {
        //pauseMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }


    void Update()
    {

        if (pauseInput && !(SceneManager.GetActiveScene().name == mainMenu))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (SceneManager.GetActiveScene().name == mainMenu)
            ResumeGame();

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        //UI.SetActive(true);

        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        //UI.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine("SetPauseFalse");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine("SetPauseFalse");
        SceneManager.LoadScene("Main Menu"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        StartCoroutine("SetPauseFalse");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
    }

    IEnumerator SetPauseFalse()
    {
        yield return new WaitForEndOfFrame();
        isPaused = false;
    }
}
