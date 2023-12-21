using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Canvas mainMenu;
    [SerializeField] Canvas settingsMenu;
    [SerializeField] Canvas creditsScreen;
    Canvas currentMenu;

    void QuitGame()
    {
        Application.Quit();
    }

    void OpenSettings()
    {
        currentMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        currentMenu = settingsMenu;
    }

    void OpenCredits()
    {
        currentMenu.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(true);
        currentMenu = creditsScreen;
    }

    void BackToMenu()
    {
        currentMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        currentMenu = mainMenu;
    }

    void Play()
    {
        SceneManager.LoadScene("Level 1 - M 1");
    }

}
