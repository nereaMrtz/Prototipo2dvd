using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalabraSecretaInteraction : MonoBehaviour
{
    [SerializeField] Canvas canvasAction;
    [SerializeField] MovingPlatform platform;
    [SerializeField] Canvas canvasUnactiveInteraction;
    bool canvasActive = false;
    bool interacted = false;
    PlayerController player;
    public float inputDelay = .25f;
    float delayLeft;

    public bool interactActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (player != null) return;
        if (other.gameObject.CompareTag("Player"))
        {
            canvasAction.gameObject.SetActive(true);
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasAction.gameObject.SetActive(false);
            canvasUnactiveInteraction.gameObject.SetActive(false);
            player = null;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            if (!interacted)
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
            else
                platform.Interact();
        }
    }

    private void Interact()
    {
        delayLeft = inputDelay;
        canvasActive = !canvasActive;
        platform.Interact();
        canvasAction.gameObject.SetActive(!canvasActive);
        interacted = true;
    }

    void UnableToInteract()
    {
        delayLeft = inputDelay;
        canvasActive = !canvasActive;
        canvasUnactiveInteraction.gameObject.SetActive(canvasActive);
        canvasAction.gameObject.SetActive(!canvasActive);
    }
}
