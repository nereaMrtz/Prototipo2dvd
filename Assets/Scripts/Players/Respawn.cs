using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Vector3 respawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12 && gameObject.layer == 9)
        {
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = respawnPos;
            this.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
