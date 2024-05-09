using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointMaster CM;
    private void Start()
    {
        CM = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player1"))
        {
            CM.SetLastCheckPointPosGhost1(coll.GetComponent<Transform>().position);
        }
        else if(coll.CompareTag("Player2"))
        {
            CM.SetLastCheckPointPosGhost2(coll.GetComponent<Transform>().position);
        }
    }
}
