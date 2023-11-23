using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton_Puerta : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject cube;

    [SerializeField] Transform finalPos;
    [SerializeField] Transform initialPos;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;

    float speed = 3.0f;

    private void OnTriggerStay(Collider other)
    {
        cube.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        if (player1.position.x >= 61.5f && player2.position.x >= 61.5f)
        {
            Debug.Log("cierate sesamo");
            Back();
        }

        else
            Move();
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
