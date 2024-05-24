using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointMaster CM;

    private bool isActive = false;
    [HideInInspector]public bool isActive1 = false;
    [HideInInspector]public bool isActive2 = false;
    [HideInInspector]public bool isOnCamera = false; 
    private bool graveMoved = false;

    [SerializeField]private Transform grave;

    [Header("Sound")]
    [SerializeField] private string clipName;
    [SerializeField] private AudioClip clip;
    private void Start()
    {
        CM = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckPointMaster>();
    }

    private void Update()
    {

        //Debug.Log(this.gameObject + " " + this.GetComponent<IsInCamera>().IsInCameraNow());
        //if (player1Passed && !isActive1)
        //{
        //    isActive1 = true;
        //    if(!graveMoved)
        //    {
        //        Vector3 pos = grave.position;
        //        pos.y += 2f;
        //        grave.DOMoveY(pos.y, 1f);
        //        graveMoved = true;
        //    }
        //    if (AudioManager.Instance.LoadSFX(clipName, clip))
        //        AudioManager.Instance.PlaySFX(clipName);
        //}
        //if (player2Passed && !isActive2) 
        //{
        //    isActive2 = true;
        //    if (!graveMoved)
        //    {
        //        Vector3 pos = grave.position;
        //        pos.y += 2f;
        //        grave.DOMoveY(pos.y, 0.7f);
        //        graveMoved = true;
        //    }
        //    if (AudioManager.Instance.LoadSFX(clipName, clip))
        //        AudioManager.Instance.PlaySFX(clipName);
        //} 
        if(this.GetComponent<IsInCamera>().IsInCameraNow() && isActive1) 
        {
            Debug.Log("Active for 1");
            CheckPointMaster.Instance.SetActiveCheckpoint1(true);
            isOnCamera = true;
        }
        if(this.GetComponent<IsInCamera>().IsInCameraNow() && isActive2) 
        {
            isOnCamera = true;
            Debug.Log("Active for 2");
            CheckPointMaster.Instance.SetActiveCheckpoint2(true);
        }
        if (!this.GetComponent<IsInCamera>().IsInCameraNow())
        {
            isOnCamera = false;
            CheckPointMaster.Instance.SetActiveCheckpoint1(false);
            CheckPointMaster.Instance.SetActiveCheckpoint2(false);
        }
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player1"))
        {
            isActive1 = true;
            CheckPointMaster.Instance.SetLastCheckPointPos(coll.GetComponent<Transform>().position);
        }
        if(coll.CompareTag("Player2"))
        {
            isActive2 = true;
            CheckPointMaster.Instance.SetLastCheckPointPos2(coll.GetComponent<Transform>().position);
            //CM.SetLastCheckPointPosGhost2(coll.GetComponent<Transform>().position);
        }
    }


   
}
