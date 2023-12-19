using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]private Transform firstPosition;
    [SerializeField]private Transform lastPosition;

    bool interacting = false;

    private void Update()
    {
        if (transform.position != firstPosition.position && interacting == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
        }
    }
    private float speed = 5.0f;
    public void Interact()
    {
        interacting = true;
        if (transform.position == lastPosition.position)
            return;
        if(transform.position != lastPosition.position) 
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
        }
    }

    public void Uninteract()
    {
        //Debug.Log("AA");
        //if (transform.position != firstPosition.position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
        //}
        interacting = false;
    }
}
