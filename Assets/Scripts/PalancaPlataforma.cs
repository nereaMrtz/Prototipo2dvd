using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PalancaPlataforma : MonoBehaviour
{

    [SerializeField] GameObject platform;
    [SerializeField] Transform p1;
    [SerializeField] Transform p2;

    [SerializeField] GameObject platform2;
    [SerializeField] Transform p1_2;
    [SerializeField] Transform p2_2;

    float speed = 3.0f;

    public bool pressed = false;

    private void OnTriggerStay(Collider other)
    {  
       this.pressed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        this.pressed = false;
    }

    public void Move()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, p2.position, speed * Time.deltaTime);
        platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, p2_2.position, speed * Time.deltaTime);
    }

    public void Back()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, p1.position, speed * Time.deltaTime);
        platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, p1_2.position, speed * Time.deltaTime);
    }
}
