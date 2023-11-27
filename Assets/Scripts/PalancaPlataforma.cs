using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
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

    private Vector3 notPressed;
    private Vector3 yesPressed;

    private AudioManager sound;

    private void Start()
    {
        notPressed = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z);
        yesPressed = new Vector3(gameObject.transform.position.x, transform.position.y -0.048f, transform.position.z);
        sound = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        sound.button.Play();
    }

    private void OnTriggerStay(Collider other)
    {
       this.pressed = true;
       transform.position = yesPressed;

       if(platform.transform.position == p1.position || platform2.transform.position == p1_2.position)
       {
            sound.door_platform.Play();
       }

       if (platform.transform.position == p2.position && platform2.transform.position == p2_2.position)
       {
           sound.door_platform.Pause();
       }
    }

    private void OnTriggerExit(Collider other)
    {
        this.pressed = false;
        this.transform.position = notPressed;
        sound.button.Play();

        sound.door_platform.Pause();
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
