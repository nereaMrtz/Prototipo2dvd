using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] public line[] dialogue;

    DialogSystem dialogSystem;

    bool triggerDone;
    public bool deleteTiggerBefore = true;

    Collider ghost;

    [SerializeField]public UnityEvent OtherEndingFunctions;
    void Start()
    {
        dialogSystem = FindAnyObjectByType<DialogSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDone)
        {
            OtherEndingFunctions.Invoke();
            if(deleteTiggerBefore)
                 Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            dialogSystem.SetDialog(this);
            dialogSystem.StartDialog(other.GetComponent<PlayerController>());
        }
    }

    public void StartDialog()
    {
        dialogSystem.SetDialog(this);
        dialogSystem.StartDialog(FindAnyObjectByType<PlayerController>());
    }

    public void SetDialogDone(bool done) { triggerDone = done; }
    public bool GetDialogDone() { return triggerDone; }
    
}
