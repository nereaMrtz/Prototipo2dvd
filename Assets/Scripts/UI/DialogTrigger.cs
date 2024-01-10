using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] public line[] dialogue;

    DialogSystem dialogSystem;

    bool triggerDone;
    void Start()
    {
        dialogSystem = FindAnyObjectByType<DialogSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDone)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            dialogSystem.SetDialog(this);
            dialogSystem.StartDialog(other);
        }
    }

    public void SetDialogDone(bool done) { triggerDone = done; }
    
}
