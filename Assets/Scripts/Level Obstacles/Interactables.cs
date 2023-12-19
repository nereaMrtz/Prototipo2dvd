using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] Canvas canvasAction;
    [SerializeField] Canvas canvasInteraction;
    bool canvasActive = false;
    [SerializeField] bool forCursed;
    PlayerContoller player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (forCursed && other.GetComponent<PlayerContoller>().curse)
            {
                canvasAction.gameObject.SetActive(true);
                player = other.GetComponent<PlayerContoller>();
            }
            else if(!forCursed && !other.GetComponent<PlayerContoller>().curse)
            {
                canvasAction.gameObject.SetActive(true);
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
                canvasAction.gameObject.SetActive(false);
                canvasInteraction.gameObject.SetActive(false);
                player = null;
            }
            else if (!forCursed && !other.GetComponent<PlayerContoller>().curse)
            {
                player = null;
                canvasAction.gameObject.SetActive(false);
                canvasInteraction.gameObject.SetActive(false);
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
        canvasActive = !canvasActive;
        canvasInteraction.gameObject.SetActive(canvasActive);
    }
}
