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
        if (SceneManager.GetActiveScene().name == "L1_M1")
            SceneManager.LoadScene("L2_M1");
        else if (SceneManager.GetActiveScene().name == "L2_M1")
            SceneManager.LoadScene("L3_M1");
    }
}
