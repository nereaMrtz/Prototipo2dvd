using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalOfGame : MonoBehaviour
{
    [SerializeField] GameObject ending;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            ending.SetActive(true);
            Destroy(this);
        }
    }
}
