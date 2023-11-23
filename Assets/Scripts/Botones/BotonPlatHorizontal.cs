using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonPlatHorizontal : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] Transform p1;
    [SerializeField] Transform p2;

    [SerializeField] GameObject platform2;
    [SerializeField] Transform p1_2;
    [SerializeField] Transform p2_2;

    [SerializeField] GameObject platform3;
    [SerializeField] Transform p1_3;
    [SerializeField] Transform p2_3;

    float speed = 5.0f;

    public bool pressed = false;

    private Vector3 notPressed;
    private Vector3 yesPressed;
    void Start()
    {
        notPressed = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z);
        yesPressed = new Vector3(gameObject.transform.position.x, transform.position.y - 0.048f, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        this.pressed = true;
        transform.position = yesPressed;
    }
    private void OnTriggerExit(Collider other)
    {
        this.pressed = false;
        this.transform.position = notPressed;
    }

    public void Move()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, p2.position, speed * Time.deltaTime);
        platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, p2_2.position, speed * Time.deltaTime);
        platform3.transform.position = Vector3.MoveTowards(platform3.transform.position, p2_3.position, speed * Time.deltaTime);
    }

    public void Back()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, p1.position, speed * Time.deltaTime);
        platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, p1_2.position, speed * Time.deltaTime);
        platform3.transform.position = Vector3.MoveTowards(platform3.transform.position, p1_3.position, speed * Time.deltaTime);
    }
}
