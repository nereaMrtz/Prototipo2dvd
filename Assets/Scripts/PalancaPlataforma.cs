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
    float speed = 3.0f;
     public  bool pressed = false;
    private bool hasInteracted = false;
    [SerializeField] bool maldita;

    // transform.position = Vector3.MoveTowards(transform.position, position1.position, _speed* Time.deltaTime);


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.action.triggered){ 
            hasInteracted = true; 
        }
        else { hasInteracted = false; }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (maldita)
        {
            if(other.gameObject.layer == 9)
            {    // if(hasInteracted){
                    pressed = !pressed; 
               // }               
            }
        }

        if (!maldita)
        {
            if (other.gameObject.layer == 8)
            {
                if (hasInteracted)
                {
                    pressed = !pressed;
                }
            }
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (maldita)
        {
            if (other.gameObject.layer == 9)
            {
                pressed = false;
            }
        }

        if (!maldita)
        {
            if (other.gameObject.layer == 8)
            {
                pressed = false;
            }
        }
    }

    private void Update()
    {
        if (pressed)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, p2.position, speed * Time.deltaTime);
        }
        else
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, p1.position, speed * Time.deltaTime);
        }

       // if (hasInteracted) { Debug.Log("interacted"); }
    }
}
