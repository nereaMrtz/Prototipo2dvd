using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 4 && gameObject.layer == 9)//Cursed Death
        {
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPos();
            this.gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else if(other.gameObject.layer == 11 && gameObject.layer == 8)//Not cursed Death
        {
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPos();
            this.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
