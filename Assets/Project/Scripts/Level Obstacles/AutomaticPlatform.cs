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
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float waitTime = 3.0f;


    private void FixedUpdate()
    {
        if (transform.position != lastPosition.position && go)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition.position, speed * Time.deltaTime);
            if (transform.position == lastPosition.position)
                StartCoroutine("ChangeDirection");
        }
        else if (transform.position != firstPosition.position && !go)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition.position, speed * Time.deltaTime);
            if (transform.position == firstPosition.position)
                StartCoroutine("ChangeDirection");
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(waitTime);
        go = !go;
    }
}
