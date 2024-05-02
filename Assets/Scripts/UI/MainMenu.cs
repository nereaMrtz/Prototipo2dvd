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
    [SerializeField] GameObject firstButtonMain;
    [SerializeField] GameObject firstButtonSettings;

    [Header ("Settings Screen")]
    [SerializeField] GameObject ControlsScreen;
    [SerializeField] GameObject SoundScreen;
    [SerializeField] GameObject CreditsScreen;
    [SerializeField] GameObject ScreenScreen;

    GameObject currentMenu;
    GameObject currentSettingScreen;

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
        currentSettingScreen = SoundScreen;
        EventSystem.current.SetSelectedGameObject(firstButtonSettings);
    }

    public void BackToMenu()
    {
        EventSystem.current.SetSelectedGameObject(firstButtonMain);
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }

    public void OpenControls()
    {
        currentSettingScreen.SetActive(false);
        ControlsScreen.SetActive(true);
        currentSettingScreen = ControlsScreen;
    }

    public void OpenSound()
    {
        currentSettingScreen.SetActive(false);
        SoundScreen.SetActive(true);
        currentSettingScreen = SoundScreen;
    }

    public void OpenCredits()
    {
        currentSettingScreen.SetActive(false);
        CreditsScreen.SetActive(true);
        currentSettingScreen=CreditsScreen;
    }

    public void OpenScreen()
    {
        currentSettingScreen.SetActive(false);
        ScreenScreen.SetActive(true);
        currentSettingScreen=ScreenScreen;
    }

    public void Play()
    {
        SceneManager.LoadScene("L2_M1");
    }

}
