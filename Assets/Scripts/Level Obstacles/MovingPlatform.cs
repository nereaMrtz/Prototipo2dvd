using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform lastPosition;

    private bool interacting = false;
    private float speed = 2.0f;

    private void FixedUpdate()
    {
        if (transform.position != firstPosition.position && interacting == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
        }
    }
    public void Interact()
    {
        interacting = true;
        if (transform.position == lastPosition.position)
        {
            return;
        }
        if (transform.position != lastPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
        }
    }

    public void Uninteract()
    {
        interacting = false;
    }
}
