using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player1") || coll.CompareTag("Player2"))
        {  
            CheckPointMaster.Instance.SetLastCheckPointPos(transform.position);
        }
    }
}
