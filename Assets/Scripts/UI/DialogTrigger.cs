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
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            dialogSystem.SetDialog(this);
            dialogSystem.StartDialog(other.GetComponent<PlayerContoller>());
        }
    }

    public void StartDialog()
    {
        dialogSystem.SetDialog(this);
        dialogSystem.StartDialog(FindAnyObjectByType<PlayerContoller>());
    }

    public void SetDialogDone(bool done) { triggerDone = done; }
    public bool GetDialogDone() { return triggerDone; }
    
}
