using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton_Puerta : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Transform initialPos;
    [SerializeField] Transform finalPos;

    [SerializeField] GameObject cube;

    float speed = 3.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            transform.position = new Vector3(gameObject.transform.position.x, transform.position.y - 0.1f, transform.position.z);
            cube.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            Move();
        }

        if(other.gameObject.layer == 11)
        {
            Back();
        }
    }

    public void Move()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, finalPos.position, speed * Time.deltaTime);
    }

    public void Back()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, initialPos.position, speed * Time.deltaTime);
    }
}
