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
    [SerializeField] private float verticalDistance = 20f;

    [SerializeField] private float shakeAmplitude = 0.7f;


    private bool shaking = false;
    private bool respawning = false;

    private void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        noise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        target1 = GameObject.FindGameObjectWithTag("Player1");
        target2 = GameObject.FindGameObjectWithTag("Player2");
    }
    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Los fantasmas estan a una distancia de: " + Mathf.Abs(target1.transform.position.y - target2.transform.position.y));
        
        if(!respawning && (Vector3.Distance(target1.transform.position, target2.transform.position) > secondDistance || Mathf.Abs(target1.transform.position.y - target2.transform.position.y) > verticalDistance))
        {           
            respawning = true;
            RespawnGhosts();
        }
        else if(Vector3.Distance(target1.transform.position, target2.transform.position) > firstDistance || Mathf.Abs(target1.transform.position.y - target2.transform.position.y) > verticalDistance)
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
