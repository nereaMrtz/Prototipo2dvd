using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class atajos : MonoBehaviour
{
     private GameObject ghost1;
     private GameObject ghost2;

    string sceneToLoad;

    private void Start()
    {
        ghost1 = GameObject.FindGameObjectWithTag("Player1");
        ghost2 = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            CheckNextLevel(SceneManager.GetActiveScene().name);
            ChangeScene(sceneToLoad);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            CheckLastLevel(SceneManager.GetActiveScene().name);
            ChangeScene(sceneToLoad);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            ghost1.GetComponent<RespawnManager>().RespawnCamera();
            ghost2.GetComponent<RespawnManager>().RespawnCamera();
        }
    }

    string CheckNextLevel(string scene)
    {
        switch (scene)
        {
            case "L1_M1":
                ghost1.GetComponent<PlayerController>().ToggleMaldicion(false);
                sceneToLoad = "L2_M1";
                break;
            case "L2_M1":
                sceneToLoad = "L3_M1";
                break;
            case "L3_M1":
                sceneToLoad = "MainMenu";
                break;
            default:
                sceneToLoad = "MainMenu";
                break;

        }
        return sceneToLoad;
    }
    string CheckLastLevel(string scene)
    {
        switch (scene)
        {
            case "L1_M1":
                sceneToLoad = "MainMenu";
                break;
            case "L2_M1":
                ghost1.GetComponent<PlayerController>().ToggleMaldicion(true);
                sceneToLoad = "L1_M1";
                break;
            case "L3_M1":
                sceneToLoad = "L2_M1";
                break;
            default:
                sceneToLoad = "MainMenu";
                break;

        }
        return sceneToLoad;
    }

    void ChangeScene(string sceneToLoad)
    {
        LevelLoader.Instance.SetLoad();
        SceneManager.LoadScene(sceneToLoad);
    }
}
