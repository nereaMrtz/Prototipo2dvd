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

    [SerializeField] private float firstDistance = 25f;
    [SerializeField] private float secondDistance = 27f;

    [SerializeField] private float shakeAmplitude = 0.7f;


    private bool shaking = false;

    private void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        noise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Los fantasmas estan a una distancia de: " + Vector3.Distance(target1.transform.position, target2.transform.position));
        
        if(Vector3.Distance(target1.transform.position, target2.transform.position) > secondDistance)
        {
            RespawnGhosts();
        }
        else if(Vector3.Distance(target1.transform.position, target2.transform.position) > firstDistance)
        {
            StartShake();
        }
        else
        {
            if(shaking)
                StopShake();
        }
    }

    void StartShake()
    {
        noise.m_AmplitudeGain = shakeAmplitude;
        shaking = true;
    }
    private void StopShake()
    {
        noise.m_AmplitudeGain = 0f;
        shaking = false;
    }

    void RespawnGhosts()
    {
        Debug.Log("A");
        target2.GetComponent<RespawnManager>().RespawnCamera();
        target1.GetComponent<RespawnManager>().RespawnCamera();
    }

}
