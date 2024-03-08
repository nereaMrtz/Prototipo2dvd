using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] RespawnManager respawManager1;
    [SerializeField] RespawnManager respawManager2;
    [SerializeField] float countdownTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = countdownTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -=Time.deltaTime;
        if (respawManager1.hasRespawned && respawManager2.hasRespawned)
        {
            ResetTimer();
        }

        if (timer <= .0f)
        {
            Debug.Log("puto");
            respawManager1.RespawnFall();
            respawManager2.RespawnFall();
        }
    }

    public void ResetTimer()
    {
        Debug.Log(1);
        timer = countdownTime;
    }
}
