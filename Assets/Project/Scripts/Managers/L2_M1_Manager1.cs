using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L2_M1_Manager : MonoBehaviour
{
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;
    [SerializeField] private float timeToChangeScene = 0.5f;
    [SerializeField] private string sceneName;

    private void Start()
    {
        ghost1 = GameObject.FindGameObjectWithTag("Player1");
        ghost2 = GameObject.FindGameObjectWithTag("Player2");
    }

    private void Update()
    {
              
        //if(Input.GetKeyDown(KeyCode.T)) {
        //    StartCoroutine("WaitThenChangeScene2");
        //}

        //if(Input.GetKeyUp(KeyCode.G))
        //{
        //    ghost1.GetComponent<RespawnManager>().RespawnCamera();
        //    ghost2.GetComponent<RespawnManager>().RespawnCamera();
        //}
    }

    void ChangeScene()
    {
        LevelLoader.Instance.SetLoad();
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator WaitThenChangeScene2() 
    {
        yield return new WaitForSeconds(timeToChangeScene);
        ChangeScene();
    }
}
