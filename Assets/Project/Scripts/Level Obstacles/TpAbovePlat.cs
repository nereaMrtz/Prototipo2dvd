using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpAbovePlat : MonoBehaviour
{
    [SerializeField] GameObject tpPos;
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("tp");
        if(other.CompareTag("Player1")|| other.CompareTag("Player2"))
        {
            other.transform.position= tpPos.transform.position;
        }
    }
}
