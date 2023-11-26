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

    private AudioManager sound;

    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    sound.door_platform.Play();
    //}

    private void OnTriggerStay(Collider other)
    {
        cube.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        if (player1.position.x >= 61.5f && player2.position.x >= 61.5f)
        {
            Back();
        }

        else
            Move();
    }

    //private void Update()
    //{
    //    if (door.transform.position == finalPos.position || door.transform.position == initialPos.position)
    //    {
    //        Debug.Log("no suena");
    //        sound.door_platform.Pause();
    //    }
    //    else
    //    {
    //        Debug.Log("si suena");
    //        sound.door_platform.Play();
    //    }
    //}

    public void Move()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, finalPos.position, speed * Time.deltaTime);
    }
    public void Back()
    {
        door.transform.position = Vector3.MoveTowards(door.transform.position, initialPos.position, speed * Time.deltaTime);
    }
}
