using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] bool forCursed;
    PlayerContoller player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (forCursed && other.GetComponent<PlayerContoller>().curse)
            {
                canvas.gameObject.SetActive(true);
                player = other.GetComponent<PlayerContoller>();
            }
            else if(!forCursed && !other.GetComponent<PlayerContoller>().curse)
            {
                canvas.gameObject.SetActive(true);
                player = other.GetComponent<PlayerContoller>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (forCursed && other.GetComponent<PlayerContoller>().curse)
            {
                canvas.gameObject.SetActive(false);
                player = null;
            }
            else if (!forCursed && !other.GetComponent<PlayerContoller>().curse)
            {
                player = null;
                canvas.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            if(player.interactInput)
            {
                Interact();
            }
        }
    }

    private void Interact() {
        Debug.Log("It's working");
    }
}
