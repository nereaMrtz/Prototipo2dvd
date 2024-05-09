using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField] private float distance = 10f;

    private Transform cameraTransform;
    private void Start()
    {
        cameraTransform = transform;
    }

    void Update()
    {
        Vector3 position = target.position;
        position.z -= distance;
        position.y += 3f;
        cameraTransform.position = position;
        //GetComponent<Transform>.transform.position.z = target.position.z - distance;
        //transform.position.y = target.position.y;
        //transform.position.x = target.position.x;

    }
    
}
