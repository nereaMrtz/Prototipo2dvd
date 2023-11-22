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

    private void OnTriggerEnter(Collider other)
    {
        //Move();
        door.SetActive(false);
        this.transform.position = new Vector3(gameObject.transform.position.x, transform.position.y - 0.1f, transform.position.z);
        cube.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    private void Update()
    {
        if (player1.position.x >= 61.5f && player2.position.x >= 61.5f)
        {
            //Back();
            door.SetActive(true);
        }
    }

    public void Move()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, finalPos.position, speed * Time.deltaTime);
    }

    public void Back()
    {
        Debug.Log("close door");
        door.transform.position = Vector3.MoveTowards(door.transform.position, initialPos.position, speed * Time.deltaTime);
    }
}
