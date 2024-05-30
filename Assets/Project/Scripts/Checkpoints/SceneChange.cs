using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ChangeScene();
    }

    public void ChangeScene()
    {
        GameObject ghost = GameObject.FindGameObjectWithTag("Player1");
        ghost.GetComponent<PlayerMovement>().SetForceModifier(Vector3.zero);
        DontDestroyOnLoad(ghost);
        GameObject ghost2 = GameObject.FindGameObjectWithTag("Player2");
        ghost2.GetComponent<PlayerMovement>().SetForceModifier(Vector3.zero);
        DontDestroyOnLoad(ghost2);
        if (SceneManager.GetActiveScene().name == "L1_M1")
            SceneManager.LoadScene("L2_M1");
        else if (SceneManager.GetActiveScene().name == "L2_M1")
            SceneManager.LoadScene("L3_M1");
        else if (SceneManager.GetActiveScene().name == "L3_M1")
            SceneManager.LoadScene("L4_M1");
        else if (SceneManager.GetActiveScene().name == "L4_M1")
        {
            Destroy(ghost);
            Destroy(ghost2);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
