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
        if (coll.CompareTag("Player1"))
        {
            CheckPointMaster.Instance.SetLastCheckPointPosGhost1(coll.GetComponent<Transform>().position);
        }
        else if(coll.CompareTag("Player2"))
        {
            CheckPointMaster.Instance.SetLastCheckPointPosGhost2(coll.GetComponent<Transform>().position);
        }
    }
}
