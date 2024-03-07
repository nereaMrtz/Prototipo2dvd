using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] AudioClip fallDeathClip;
    [SerializeField] string fallDeathClipName;
    [SerializeField] AudioClip damagelDeathClip;
    [SerializeField] string damagelDeathClipName;
    public bool hasRespawned { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && gameObject.layer == 9)//Cursed Death
            RespawnFall();
        else if (other.gameObject.layer == 11 && gameObject.layer == 8)//Not cursed Death        
            RespawnFall();
        else if (other.gameObject.layer == 12)//All Death        
            RespawnFall();


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4 && gameObject.layer == 9)//Cursed Death
            RespawnDamage();
        else if (collision.gameObject.layer == 11 && gameObject.layer == 8)//Not cursed Death        
            RespawnDamage();
        else if (collision.gameObject.layer == 12)//All Death        
            RespawnDamage();
    }

    public void RespawnFall()
    {
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPos();
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(fallDeathClipName, fallDeathClip);
        AudioManager.Instance.PlaySFX(fallDeathClipName);
    }

    public void RespawnDamage()
    {
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPos();
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(damagelDeathClipName, damagelDeathClip);
        AudioManager.Instance.PlaySFX(damagelDeathClipName);
    }

    IEnumerator HasRespawnedBoolean()
    {
        yield return new WaitForNextFrameUnit();
        Debug.Log(2);
        hasRespawned = false;
    }
}
