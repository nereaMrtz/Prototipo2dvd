using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private GameObject target1;
    [SerializeField]private GameObject target2;

    private CinemachineVirtualCamera camera;
    private CinemachineBasicMultiChannelPerlin noise;    

    [SerializeField] private float Distance = 27f;
    [SerializeField] private float verticalDistance = 20f;

    private bool shaking = false;
    private bool respawning = false;

    private void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        noise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        target1 = GameObject.FindGameObjectWithTag("Player1");
        target2 = GameObject.FindGameObjectWithTag("Player2");       

    }


    void RespawnGhosts()
    {

        if (target2 != null && target2.GetComponent<RespawnManager>() != null)
        {                
            target2.GetComponent<RespawnManager>().RespawnCamera();
        }

        if (target1 != null && target1.GetComponent<RespawnManager>() != null)
        {
            target1.GetComponent<RespawnManager>().RespawnCamera();
        }

        StartCoroutine("RespawningBoolean");
    }

    IEnumerator RespawningBoolean()
    {
        yield return new WaitForSeconds(3f);
        respawning = false;
    }
}
