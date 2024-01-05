using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalabraSecretaInteraction : MonoBehaviour
{
    [SerializeField] Canvas canvasAction;
    [SerializeField] Canvas canvasInteraction;
    [SerializeField] Canvas canvasUnactiveInteraction;
    bool canvasActive = false;
    PlayerContoller player;
    public float inputDelay = .25f;
    float delayLeft;

    public bool interactActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (player != null) return;
        if (other.gameObject.CompareTag("Player"))
        {
                canvasAction.gameObject.SetActive(true);
                player = other.GetComponent<PlayerContoller>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                canvasAction.gameObject.SetActive(false);
                canvasInteraction.gameObject.SetActive(false);
                player = null;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            if (player.interactInput && delayLeft <= .0f)
            {
                if (interactActive)
                    Interact();
                else
                {
                    UnableToInteract();
                }
            }
            delayLeft -= Time.deltaTime;
        }
    }

    private void Interact()
    {
        delayLeft = inputDelay;
        canvasActive = !canvasActive;
        canvasInteraction.gameObject.SetActive(canvasActive);
        canvasAction.gameObject.SetActive(!canvasActive);
    }

    void UnableToInteract()
    {
        delayLeft = inputDelay;
        canvasActive = !canvasActive;
        canvasUnactiveInteraction.gameObject.SetActive(canvasActive);
        canvasAction.gameObject.SetActive(!canvasActive);
    }
}
