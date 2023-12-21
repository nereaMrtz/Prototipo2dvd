using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 4 && gameObject.layer == 9)
        {
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPos();
            this.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
