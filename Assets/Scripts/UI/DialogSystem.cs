using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public enum TYPE { MALDITO, FANTASMA, BOLA, FOG, BOOB };
[System.Serializable] public struct line { public string text; public TYPE type; }

public class DialogSystem : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject q;

    line[] dialogue;

    int index = 0;

    bool startText;
    bool typingText;
    bool endDialog;

    bool gamepad;

    [SerializeField] float wordSpeed;

    Coroutine typing;


    [SerializeField] RawImage ghostImage;
    [SerializeField] Texture maldito;
    [SerializeField] Texture fantasma;
    [SerializeField] Texture bolaCristal;
    [SerializeField] Texture fog;
    [SerializeField] Texture boob;

    PlayerController ghost;
    DialogTrigger trigger;
    void Start()
    {
        dialogueText.text = "";
        q.SetActive(false);

    }

    public void StartDialog(PlayerController other)
    {
        startText = true;
        ghost = other;
        ghost.SetStopMovement(true);
    }

    private void Update()
    {
        if (startText)
        {

            if (!dialogueBox.activeInHierarchy)
            {
                endDialog = false;
                dialogueBox.SetActive(true);
                typing = StartCoroutine(Typing());

            }
            else if (dialogueText.text == dialogue[index].text && Input.GetKeyDown(KeyCode.Q) || dialogueText.text == dialogue[index].text && gamepad == true)
            {

                NextLine();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                startText = false;
            }

            if (dialogue[index].type == TYPE.MALDITO)
            {
                ghostImage.texture = maldito;
            }
            else if (dialogue[index].type == TYPE.FANTASMA)
            {
                ghostImage.texture = fantasma;
            }
            else if (dialogue[index].type == TYPE.BOLA)
            {
                ghostImage.texture = bolaCristal;
            }
            else if (dialogue[index].type == TYPE.FOG)
            {
                ghostImage.texture = fog;
            }
            else if (dialogue[index].type == TYPE.BOOB)
            {
                ghostImage.texture = boob;
            }

        }
        else
        {
            RemoveText();
            if (ghost != null)
            {
                ghost.SetStopMovement(false);
                trigger.SetDialogDone(true);

            }

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


    public void SetDialog(DialogTrigger dTrigger)
    {
        dialogue = dTrigger.dialogue;
        trigger = dTrigger;
    }

    public bool GetEndDialog() { return endDialog; }

    ///////////PLAYER ACTION INPUT
    public void Next(InputAction.CallbackContext context)
    {
        float aux = context.ReadValue<float>();
        if (aux != 0f)
        {
            gamepad = true;
        }
        else { gamepad = false; }
    }
}
