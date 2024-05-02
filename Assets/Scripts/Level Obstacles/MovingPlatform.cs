using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform lastPosition;

    private bool interacting = false;
    private float speed = 2.0f;

    public AudioClip platformClip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = platformClip;
        audioSource.loop = true;
    }

    private void FixedUpdate()
    {
        if (transform.position != firstPosition.position && interacting == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
            audioSource.Play();
        }
        else if (!interacting)
        {
            audioSource.Stop();
        }
    }
    public void Interact()
    {
        interacting = true;
        if (transform.position == lastPosition.position)
        {
            audioSource.Stop();
            return;
        }
        if (transform.position != lastPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
            audioSource.Play();
        }
    }

    public void Uninteract()
    {
        interacting = false;
    }
}
