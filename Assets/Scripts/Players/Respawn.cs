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
            Debug.Log("awita");
            gameObject.GetComponent<CharacterController>().Move(respawnPos - gameObject.transform.position);
        }
    }
}
