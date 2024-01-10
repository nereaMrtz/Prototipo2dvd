using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_M1_Manager : ScenesManager
{
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;
    [SerializeField] private float timeToChangeScene = 2;

    private void Update()
    {
        if(ghost1.layer == 9 && ghost2.layer == 9)
        {
            ChangeScene();
        }
        if(Input.GetKeyUp(KeyCode.T)) 
        {
            LevelLoader.Instance.SetLoad();
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
