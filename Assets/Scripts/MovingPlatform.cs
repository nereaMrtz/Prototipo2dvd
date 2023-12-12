using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]private Transform firstPosition;
    [SerializeField]private Transform lastPosition;

    private float speed = 5.0f;
    public void Interact()
    {
        if(transform.position != lastPosition.position) 
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
        }
    }

    public void Uninteract()
    {
        if (transform.position != firstPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
        }
    }
}
