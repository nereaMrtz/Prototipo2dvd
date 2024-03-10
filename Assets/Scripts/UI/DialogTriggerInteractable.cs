using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogTriggerInteractable : MonoBehaviour
{
    [SerializeField] public line[] dialogue;
    [SerializeField] GameObject pressButton;

    DialogSystem dialogSystem;

    bool triggerDone;

    Collider ghost;


    [SerializeField] public UnityEvent OtherEndingFunctions;
    void Awake()
    {
        dialogSystem = FindAnyObjectByType<DialogSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDone)
        {
            OtherEndingFunctions.Invoke();
            Destroy(this.gameObject.GetComponent<BoxCollider>());
            Destroy(pressButton);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            pressButton.SetActive(true);
            if (dialogSystem.GetGamepad() || Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("entro entro");
                dialogSystem.SetDialogInteractable(this);
                dialogSystem.StartDialog(other.GetComponent<PlayerController>());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pressButton.SetActive(false);
    }

    public void StartDialog()
    {
        dialogSystem.SetDialogInteractable(this);
        dialogSystem.StartDialog(FindAnyObjectByType<PlayerController>());
    }

    public void SetDialogDone(bool done) { triggerDone = done; }
    public bool GetDialogDone() { return triggerDone; }

}
