using System;
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
        currentMenu= mainMenu;
}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        currentMenu.SetActive(false);
        settingsMenu.SetActive(true);
        currentMenu = settingsMenu;
    }

    public void OpenCredits()
    {
        currentMenu.SetActive(false);
        creditsScreen.SetActive(true);
        currentMenu = creditsScreen;
    }

    public void BackToMenu()
    {
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }

    public void BackToSettings()
    {
        Debug.Log(1);
        currentMenu.SetActive(false);
        settingsMenu.SetActive(true);
        currentMenu = settingsMenu;
    }

    public void Play()
    {
        SceneManager.LoadScene("L1_M1");
    }

}
