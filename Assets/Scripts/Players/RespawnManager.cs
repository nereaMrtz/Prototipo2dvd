using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public bool hasRespawned { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && gameObject.layer == 9)
            Respawn();
    }

    public void Respawn()
    {
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPos();
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
    }

    IEnumerator HasRespawnedBoolean()
    {
        yield return new WaitForNextFrameUnit();
        Debug.Log(2);
        hasRespawned = false;
    }
}
