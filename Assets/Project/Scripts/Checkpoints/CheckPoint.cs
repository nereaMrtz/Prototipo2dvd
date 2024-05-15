using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointMaster CM;

    private bool isActive = false; 
    private bool isOnCamera = false; 
    private bool player1Passed = false; 
    private bool player2Passed = false;

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
        if(player1Passed && player2Passed && !isActive) 
        {
            isActive = true;
            Vector3 pos = grave.position;
            pos.y += 2f;
            grave.DOMoveY(pos.y, 1f);
            if (AudioManager.Instance.LoadSFX(clipName, clip))
                AudioManager.Instance.PlaySFX(clipName);
        }    
        if(this.GetComponent<IsInCamera>().IsInCameraNow() && isActive) 
        {
            CM.SetActiveCheckpoint(true);
        }
        else
        {
            CM.SetActiveCheckpoint(false);
        }
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player1"))
        {
            player1Passed = true;
            CM.SetLastCheckPointPos(coll.GetComponent<Transform>().position);
        }
        else if(coll.CompareTag("Player2"))
        {
            CM.SetLastCheckPointPos2(coll.GetComponent<Transform>().position);
            player2Passed = true;
            //CM.SetLastCheckPointPosGhost2(coll.GetComponent<Transform>().position);
        }
    }
}
