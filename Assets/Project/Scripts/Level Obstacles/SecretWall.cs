using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWall : MonoBehaviour
{
    [SerializeField] GameObject wall;


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            wall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            wall.SetActive(true);
        }
    }
}
