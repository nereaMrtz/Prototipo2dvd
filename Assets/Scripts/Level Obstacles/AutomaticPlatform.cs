using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticPlatform : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform lastPosition;

    private bool go = true;
    [Header("Parameters")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float waitTime = 3.0f;


    public AudioClip platformClip;
    public AudioSource audioSource;
    private void Start()
    {
        audioSource.clip = platformClip;
        audioSource.loop = true;
    }

    private void FixedUpdate()
    {
        if (transform.position != lastPosition.position && go)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
            if (transform.position == lastPosition.position)
                StartCoroutine("ChangeDirection");
            audioSource.Play();
        }
        else if (transform.position != firstPosition.position && !go)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
            if (transform.position == firstPosition.position)
                StartCoroutine("ChangeDirection");
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(waitTime);
        go = !go;
    }
}
