using System;
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
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        if(this.gameObject.tag == "Player1")
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPosGhost1();
        else if(this.gameObject.tag == "Player2")
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPosGhost2();
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(fallDeathClipName, fallDeathClip);
        AudioManager.Instance.PlaySFX(fallDeathClipName);
    }

    public void RespawnDamage()
    {
        Debug.Log(this.gameObject);
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        if (this.gameObject.tag == "Player1")
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPosGhost1();
        else if (this.gameObject.tag == "Player2")
            this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPosGhost2();
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(damagelDeathClipName, damagelDeathClip);
        AudioManager.Instance.PlaySFX(damagelDeathClipName);
    }

    public void RespawnCamera()
    {

        try
        {

            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            if (this.gameObject.tag == "Player1")
            {
                Debug.Log("Respawn player 1");
                this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPosGhost1();
            }
            else if (this.gameObject.tag == "Player2")
            {
                Debug.Log("Respawn player 2");
                this.gameObject.transform.position = CheckPointMaster.Instance.GetLastCheckPointPosGhost2(); 
            }
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            hasRespawned = true;
            StartCoroutine(HasRespawnedBooleanCamera());
            //AudioManager.Instance.LoadSFX(damagelDeathClipName, damagelDeathClip);
            //AudioManager.Instance.PlaySFX(damagelDeathClipName);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error en RespawnCamera(): " + ex.Message);
        }
    }
    IEnumerator HasRespawnedBoolean()
    {
        yield return new WaitForNextFrameUnit();
        hasRespawned = false;
    }
    IEnumerator HasRespawnedBooleanCamera()
    {
        yield return new WaitForNextFrameUnit();
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        hasRespawned = false;
    }
}
