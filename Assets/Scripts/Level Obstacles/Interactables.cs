using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] Canvas canvasAction;
    [SerializeField] Canvas canvasInteraction;
    bool canvasActive = false;
    [SerializeField] bool forCursed;
    PlayerController player;
    public float inputDelay = .25f;
    float delayLeft;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (forCursed && other.GetComponent<PlayerController>().curse)
            {
                canvasAction.gameObject.SetActive(true);
                player = other.GetComponent<PlayerController>();
            }
            else if(!forCursed && !other.GetComponent<PlayerController>().curse)
            {
                canvasAction.gameObject.SetActive(true);
                player = other.GetComponent<PlayerController>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            if (forCursed && other.GetComponent<PlayerController>().curse)
            {
                canvasAction.gameObject.SetActive(false);
                canvasInteraction.gameObject.SetActive(false);
                player = null;
            }
            else if (!forCursed && !other.GetComponent<PlayerController>().curse)
            {
                player = null;
                canvasAction.gameObject.SetActive(false);
                canvasInteraction.gameObject.SetActive(false);
            }
            delayLeft = .0f;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            if(player.interactInput && delayLeft<=.0f)
            {
                Interact();
            }
            delayLeft -= Time.deltaTime;
        }
    }

    private void Interact() {
        delayLeft = inputDelay;
        canvasActive = !canvasActive;
        canvasInteraction.gameObject.SetActive(canvasActive);
        canvasAction.gameObject.SetActive(!canvasActive);
    }
}
