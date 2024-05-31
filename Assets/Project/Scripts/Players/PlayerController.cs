using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.InputSystem.DefaultInputActions;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

using System.Runtime.CompilerServices;
using UnityEngine.VFX;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerController : MonoBehaviour
{
    public bool interactInput { get; private set; }
    public PlayerMovement pMovement;

    [Header("Curse")]

    [SerializeField] public PlayerController otherPlayer;
    public bool curse;

    [SerializeField] Material normalMat;
    [SerializeField] Material cursedMat;
    [SerializeField] ParticleSystem ghostParticles;

    [HideInInspector] public bool isFirstLevel = false;


    [SerializeField] AudioClip jumpClip;
    [SerializeField] string jumpClipName;
    [SerializeField] AudioClip curseChangeClip;
    [SerializeField] string curseChangeClipName;

    [SerializeField] VisualEffect curseEffect;
    [SerializeField] GameObject cursedEffect;

    [SerializeField] private Animator animator;
    private void Start()
    {
        cursedEffect.SetActive(false);

        if (SceneManager.GetActiveScene().name == "L1_M1" /*|| SceneManager.GetActiveScene().name == "VFX Scene"*/)
            isFirstLevel = true;
        else
            isFirstLevel = false;

        if (!curse)
        {
            ghostParticles.Play();
        }
        else
        {
            cursedEffect.SetActive(true);
        }

        if (pMovement == null)
        {
            pMovement = GetComponentInParent<PlayerMovement>();
            pMovement.curse = curse;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        float aux = context.ReadValue<float>();
        if (aux != 0f)
        {
            interactInput = true;
        }
        else { interactInput = false; }
    }

    public void OnMaldicion(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            if (!otherPlayer.curse)
            {
                otherPlayer.ChangeMaldicion();
                ChangeMaldicion();
            }
            //sound.maldicion.Play();
        }
    }

    // LAYERS
    // 8: Not Cursed
    // 9: Cursed

    //"This changes the curse and layers that it collides with"
    public void ChangeMaldicion()
    {
        if (isFirstLevel)
        {
            return;
        }
        curse = !curse;
        if (curse)
        {
            curseEffect.Play();
            this.gameObject.layer = 9;
            ghostParticles.Stop();
            cursedEffect.SetActive(true);

        }
        else
        {
            cursedEffect.SetActive(false);
            this.gameObject.layer = 8;
            ghostParticles.Play();
        }
        pMovement.curse = curse;

        AudioManager.Instance.LoadSFX(curseChangeClipName, curseChangeClip);
        AudioManager.Instance.PlaySFX(curseChangeClipName);
    }

    public void SetStopMovement(bool set)
    {
        if (set)
            pMovement.movementEnabled = false;
        else
            pMovement.movementEnabled = true;
    }

    public void ToggleMaldicion(bool active)
    {
        if (!active)
        {
            if (curse)
            {
                curse = active;
                curseEffect.Play();
                this.gameObject.layer = 9;
                ghostParticles.Stop();
                pMovement.curse = curse;
            }
            cursedEffect.SetActive(true);
            if (otherPlayer.curse)
            {
                otherPlayer.curse = active;
                otherPlayer.curseEffect.Play();
                otherPlayer.gameObject.layer = 9;
                otherPlayer.ghostParticles.Stop();
                otherPlayer.pMovement.curse = curse;
            }
        }
        else
        {
            curse = active;
            cursedEffect.SetActive(false);
            this.gameObject.layer = 8;
            ghostParticles.Play();
            pMovement.curse = curse;
        }
    }
}
