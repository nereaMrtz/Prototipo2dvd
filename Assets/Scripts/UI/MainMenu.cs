using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject firstButtonMain;
    [SerializeField] GameObject firstButtonSettings;
    [SerializeField] GameObject firstButtonCredits;
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
        EventSystem.current.SetSelectedGameObject(firstButtonSettings);
    }

    public void OpenCredits()
    {
        currentMenu.SetActive(false);
        creditsScreen.SetActive(true);
        currentMenu = creditsScreen;
        EventSystem.current.SetSelectedGameObject(firstButtonCredits);

    }

    public void BackToMenu()
    {
        EventSystem.current.SetSelectedGameObject(firstButtonMain);
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }

    public void BackToSettings()
    {
        Debug.Log(1);
        EventSystem.current.SetSelectedGameObject(firstButtonSettings);
        currentMenu.SetActive(false);
        settingsMenu.SetActive(true);
        currentMenu = settingsMenu;
    }

    public void Play()
    {
        SceneManager.LoadScene("L1_M1");
    }

}
