using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject creditsScreen;
    GameObject currentMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsScreen.SetActive(false);
}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        currentMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        currentMenu = settingsMenu;
    }

    public void OpenCredits()
    {
        currentMenu.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(true);
        currentMenu = creditsScreen;
    }

    public void BackToMenu()
    {
        currentMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        currentMenu = mainMenu;
    }

    public void Play()
    {
        SceneManager.LoadScene("L1_M1");
    }

}
