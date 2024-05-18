using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBoxTrigger : MonoBehaviour
{
    public BoxRespawn bRespawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            bRespawn.Respawn();
        }
    }
}
