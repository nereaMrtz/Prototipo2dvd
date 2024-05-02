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
using UnityEngine.TextCore.Text;
using UnityEditor.Rendering;

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
    bool typingText = false;
    bool endDialog;

    bool gamepad;

    bool isInteractable;

    float wordSpeed = 0.03f;

    Coroutine typing;


    [SerializeField] RawImage ghostImage;
    [SerializeField] Texture maldito;
    [SerializeField] Texture fantasma;
    [SerializeField] Texture bolaCristal;
    [SerializeField] Texture fog;
    [SerializeField] Texture boob;

    [SerializeField] TMP_FontAsset defaultFont;
    [SerializeField] Material defaultFontMaterial;
    [SerializeField] TMP_FontAsset BoFont;
    [SerializeField] Material BoFontMaterial;
    [SerializeField] TMP_FontAsset ObFont;
    [SerializeField] Material ObFontMaterial;

    PlayerController ghost;
    DialogTrigger trigger;
    DialogTriggerInteractable triggerI;
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
        other.otherPlayer.SetStopMovement(true);
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
                gamepad = false;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && typingText || gamepad == true && typingText)
            {
                dialogueText.text = dialogue[index].text;
                typingText = false;
                q.SetActive(true);
                StopCoroutine(typing);  
                gamepad = false;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                startText = false;
            }


            //******** CAMBIO DE IMAGEN Y TEXTO

            switch (dialogue[index].type)
            {
                case TYPE.MALDITO:

                    ghostImage.texture = maldito;
                    dialogueText.font = BoFont;
                    dialogueText.fontSharedMaterial = BoFontMaterial;
                    dialogueText.outlineColor = Color.red;
                    break;

                case TYPE.FANTASMA:

                    ghostImage.texture = fantasma;
                     dialogueText.font = ObFont;
                    dialogueText.fontSharedMaterial = ObFontMaterial;
                    dialogueText.outlineColor = Color.cyan;
                    break;

                case TYPE.BOLA:

                    ghostImage.texture = bolaCristal;
                    dialogueText.font = defaultFont;
                    dialogueText.fontSharedMaterial = defaultFontMaterial;
                    dialogueText.outlineColor = Color.magenta;
                    break;

                case TYPE.FOG:

                    ghostImage.texture = fog;
                    dialogueText.font = defaultFont;
                    dialogueText.fontSharedMaterial = defaultFontMaterial;
                    dialogueText.outlineColor = Color.magenta;
                    break;

                case TYPE.BOOB:

                    ghostImage.texture = boob;
                    dialogueText.font = defaultFont;
                    dialogueText.fontSharedMaterial = defaultFontMaterial;
                    dialogueText.outlineColor = Color.black;
                    break;

                default:
                    break;
            }
        }
        else
        {
            RemoveText();
            if (ghost != null)
            {
                ghost.SetStopMovement(false);
                ghost.otherPlayer.SetStopMovement(false);
                if(!isInteractable)
                 trigger.SetDialogDone(true);
                else
                    triggerI.SetDialogDone(true);
                    startText = false;
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
        wordSpeed = 0.03f;
        foreach (char letter in dialogue[index].text.ToCharArray())
        {
            typingText = true;
            dialogueText.text += letter;
            // Dialogue sound
            yield return new WaitForSeconds(wordSpeed);
        }
        typingText = false;
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
        isInteractable = false;
    }
    
    public void SetDialogInteractable(DialogTriggerInteractable dTrigger)
    {
        dialogue = dTrigger.dialogue;
        triggerI = dTrigger;
        isInteractable = true;
    }

    public bool GetEndDialog() { return endDialog; }
    public bool GetGamepad() { return gamepad; }

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
