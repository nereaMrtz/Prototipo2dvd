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

    [SerializeField] private Animator animator;

    public bool hasRespawned { get; private set; }


    private void Update()
    {
       
        if(!this.gameObject.GetComponent<IsInCamera>().IsInCameraNow())
        {
            RespawnCamera2();           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && gameObject.layer == 9)//Cursed Death
        {
            if(this.gameObject.tag == "Player1")
            {
                if(CheckPointMaster.Instance.ActiveCheckpoint1())
                { 
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
        else if (other.gameObject.layer == 11 && gameObject.layer == 8)//Not cursed Death
        {
            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
        else if (other.gameObject.layer == 12)//All Death
        {

            if (this.gameObject.tag == "Player1")
            {
                
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 4 && gameObject.layer == 9)//Cursed Death
        {

            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
        else if (other.gameObject.layer == 11 && gameObject.layer == 8)//Not cursed Death
        {
            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
        else if (other.gameObject.layer == 12)//All Death
        {

            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnFall();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4 && gameObject.layer == 9)//Cursed Death
        {
            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnDamage();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnDamage();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }            
        else if (collision.gameObject.layer == 11 && gameObject.layer == 8)//Not cursed Death        
        {
            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnDamage();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnDamage();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
        else if (collision.gameObject.layer == 12)//All Death        
        {
            if (this.gameObject.tag == "Player1")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint1())
                {
                    RespawnDamage();
                }
                else
                {
                    RespawnCamera2();
                }
            }
            if (this.gameObject.tag == "Player2")
            {
                if (CheckPointMaster.Instance.ActiveCheckpoint2())
                {
                    RespawnDamage();
                }
                else
                {
                    RespawnCamera2();
                }
            }
        }
    }

    public void RespawnFall()
    {
        //animator.SetBool("IsDying", true);
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        if(this.gameObject.tag == "Player1")
            this.gameObject.transform.position = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>().GetLastCheckPointPosGhost1();
        else if(this.gameObject.tag == "Player2")
            this.gameObject.transform.position = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>().GetLastCheckPointPosGhost2();

        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(fallDeathClipName, fallDeathClip);
        AudioManager.Instance.PlaySFX(fallDeathClipName);
    }

    public void RespawnDamage()
    {
        //animator.SetBool("IsDying", true);
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        if (this.gameObject.tag == "Player1")
            this.gameObject.transform.position = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>().GetLastCheckPointPosGhost1();
        else if (this.gameObject.tag == "Player2")
            this.gameObject.transform.position = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>().GetLastCheckPointPosGhost2();
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(damagelDeathClipName, damagelDeathClip);
        AudioManager.Instance.PlaySFX(damagelDeathClipName);
    }

    public void RespawnCamera2()
    {
        GameObject ghost1 = GameObject.FindWithTag("Player1");
        GameObject ghost2 = GameObject.FindWithTag("Player2");
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        if (this.gameObject.tag == "Player1")
        {
            Vector3 position = ghost2.transform.position;
            position.y += 2.5f;
            this.gameObject.transform.position = position;
        }
        else if (this.gameObject.tag == "Player2")
        {
            Vector3 position = ghost1.transform.position;
            position.y += 2.5f;
            this.gameObject.transform.position = position;
        }
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        hasRespawned = true;
        StartCoroutine(HasRespawnedBoolean());
        AudioManager.Instance.LoadSFX(fallDeathClipName, fallDeathClip);
        AudioManager.Instance.PlaySFX(fallDeathClipName);
    }
    public void RespawnCamera()
    {
        try
        {
            //animator.SetBool("IsDying", true);
            
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            if (this.gameObject.tag == "Player1")
            {
                this.gameObject.transform.position = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>().GetLastCheckPointPosGhost1();
            }
            else if (this.gameObject.tag == "Player2")
            {
                this.gameObject.transform.position = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>().GetLastCheckPointPosGhost2(); 
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
