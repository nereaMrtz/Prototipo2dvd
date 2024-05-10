using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    bool prevInput;
    // public GameObject UI;
    public HUDUI hudui;

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
        if (pauseInput && !prevInput)
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
        prevInput = pauseInput;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        hudui.ShowPauseMenuHud();
        EventSystem.current.SetSelectedGameObject(firstPauseButton);

        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        hudui.anim.Play("hide"); 
        Time.timeScale = 1;
        StartCoroutine("SetPauseFalse");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine("SetPauseFalse");
        Destroy(GameObject.FindGameObjectWithTag("Camera"));
        SceneManager.LoadScene(mainMenu); 
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
