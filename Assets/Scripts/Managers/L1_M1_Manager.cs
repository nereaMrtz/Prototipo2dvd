using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_M1_Manager : ScenesManager
{
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;
    [SerializeField] private float timeToChangeScene = 0.5f;

    private void Update()
    {
        if(ghost1.layer == 9 && ghost2.layer == 9)
        {
            StartCoroutine("WaitThenChangeScene");
        }      
        
        if(Input.GetKeyDown(KeyCode.T)) {
            StartCoroutine("WaitThenChangeScene");
        }
    }

    protected override void ChangeScene()
    {        
        base.ChangeScene();
    }

    IEnumerator WaitThenChangeScene() 
    {
        yield return new WaitForSeconds(timeToChangeScene);
        ChangeScene();
    }
}
