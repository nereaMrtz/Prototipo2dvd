using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] string[] dialogue;
    int index = 0;

    [SerializeField] bool startText;
    bool typingText;

    [SerializeField] float wordSpeed;

    Coroutine typing;

    [SerializeField] GameObject q;

    void Start()
    {
        dialogueText.text = "";
        q.SetActive(false);  
    }

    private void Update()
    {
       if(startText)
        {
            if (!dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(true);
                typing = StartCoroutine(Typing());

            }
            else if (dialogueText.text == dialogue[index] && Input.GetKeyDown(KeyCode.Q))
            {

                NextLine();
            }
            /*if (Input.GetKeyDown(KeyCode.Q) && dialogueBox.activeInHierarchy)
            {
                RemoveText();
            }*/
        }
        else
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        if (typing != null) { StopCoroutine(typing); }
        dialogueText.text = "";
        index = 0;
        dialogueBox.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            // Dialogue sound
            yield return new WaitForSeconds(wordSpeed);
        }
        q.SetActive(true);
    }

    public void NextLine()
    {
        q.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typing = StartCoroutine(Typing());
        }
        else
        {
            startText = false;
            if (typing != null) { StopCoroutine(typing); }
            dialogueText.text = "";
            index = 0;
            dialogueBox.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
           startText = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
           startText = false;
            RemoveText();
        }
    }
}
