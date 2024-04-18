using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMainMenu : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("L1_M1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
