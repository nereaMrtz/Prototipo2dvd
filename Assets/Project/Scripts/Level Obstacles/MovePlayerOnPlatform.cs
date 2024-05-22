using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnPlatform : MonoBehaviour
{
    [SerializeField] Transform platformTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fog")) 
        other.transform.SetParent(platformTransform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
            DontDestroyOnLoad(other);
    }
}
