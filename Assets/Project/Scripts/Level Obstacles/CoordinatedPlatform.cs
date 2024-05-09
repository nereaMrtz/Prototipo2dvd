using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatedPlatform : MonoBehaviour
{
    [SerializeField] CoordinatedPlatform otherPlatform;
    public bool isReady;

    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform lastPosition;

    private bool interacting = false;
    private float speed = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1")|| other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("trigger");
            isReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            isReady = false;
        }
    }

    void FixedUpdate()
    {
        if(isReady && otherPlatform.isReady)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (transform.position == lastPosition.position)
        {
            return;
        }
        if (transform.position != lastPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
        }
    }
}
