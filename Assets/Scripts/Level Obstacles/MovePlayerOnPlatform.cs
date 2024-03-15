using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnPlatform : MonoBehaviour
{
    [SerializeField] Transform platformTransform;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        other.transform.SetParent(platformTransform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
