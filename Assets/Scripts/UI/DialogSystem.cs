using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum TYPE { MALDITO, FANTASMA};
 [System.Serializable]public struct line{ public string text; public TYPE type;}

public class DialogSystem : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] public line[] dialogue;

    int index = 0;

    [SerializeField] bool startText;
    bool typingText;

    [SerializeField] float wordSpeed;

    Coroutine typing;

    [SerializeField] GameObject q;

    [SerializeField] RawImage ghostImage;
    [SerializeField] Texture maldito;
    [SerializeField] Texture fantasma;

     PlayerContoller ghost;

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
            else if (dialogueText.text == dialogue[index].text && Input.GetKeyDown(KeyCode.Q))
            {

                NextLine();
            }
            /*if (Input.GetKeyDown(KeyCode.Q) && dialogueBox.activeInHierarchy)
            {
                RemoveText();
            }*/

            if (dialogue[index].type == TYPE.MALDITO)
            {
                ghostImage.texture = maldito;
            }
            else if (dialogue[index].type == TYPE.FANTASMA)
            {
                ghostImage.texture = fantasma;
            }

        }
        else
        {
            RemoveText();
            if(ghost != null)
            {
                ghost.UnfreezePosition();
            }
           // this.gameObject.GetComponent<Collider>().enabled = false;
           
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
        foreach (char letter in dialogue[index].text.ToCharArray())
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
        {
            startText = true;
            ghost = other.GetComponent<PlayerContoller>();
            ghost.FreezePosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
           startText = false;
            RemoveText();
        }
    }
}
