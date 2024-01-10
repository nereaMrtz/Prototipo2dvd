using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_M1_Manager : ScenesManager
{
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;

    private void Update()
    {
        if(ghost1.layer == 9 && ghost2.layer == 9)
        {

        }
    }

    protected override void ChangeScene()
    {        
        base.ChangeScene();
    }
}
