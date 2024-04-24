using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject creditsMenu;
    [Header("Buttons")]
    [SerializeField] GameObject firstButtonMain;
    [SerializeField] GameObject firstButtonSettings;
    [SerializeField] GameObject firstButtonCredits;

    GameObject currentMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
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
        creditsMenu.SetActive(true);
        currentMenu = creditsMenu;
        EventSystem.current.SetSelectedGameObject(firstButtonCredits);
    }

    public void BackToMenu()
    {
        EventSystem.current.SetSelectedGameObject(firstButtonMain);
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }


    public void Play()
    {
        SceneManager.LoadScene("L1_M1");
    }

}
