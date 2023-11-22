using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Transform initialPos;
    [SerializeField] Transform finalPos;

    float speed = 3.0f;

    private Vector3 notPressed;
    private Vector3 yesPressed;

    private void Start()
    {
        notPressed = new Vector3(gameObject.transform.position.x, transform.position.y, transform.position.z);
        yesPressed = new Vector3(gameObject.transform.position.x, transform.position.y - 0.048f, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        transform.position = yesPressed;
        Move();
    }
    
    //LA PUERTA NO SE CIERRA
    private void OnTriggerExit(Collider other)
    {
        transform.position = notPressed;
        Back();
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
