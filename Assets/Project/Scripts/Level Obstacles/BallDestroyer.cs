using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            other.GetComponent<SphereRespawn>().Respawn();
        }
    }
}
