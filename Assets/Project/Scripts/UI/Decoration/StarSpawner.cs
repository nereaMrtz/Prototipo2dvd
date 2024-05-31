using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    float time;
    float timer;

    public GameObject _star;
    void RestartTimer()
    {
        time = Random.Range(minTime, maxTime);
        timer = 0.0f;
    }

    private void Start()
    {
        RestartTimer();
    }

    private void Update()
    {
        if (timer>time)
        {
            GameObject star = Object.Instantiate(_star);
            star.transform.position = this.transform.position;
            RestartTimer();
        }

        timer += Time.deltaTime;
    }
}
