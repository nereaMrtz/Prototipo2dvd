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

    private float distance;

    [SerializeField] AudioClip jumpClip;
    [SerializeField] string jumpClipName;
    [SerializeField] AudioClip curseChangeClip;
    [SerializeField] string curseChangeClipName;

    [SerializeField] VisualEffect curseEffect;

    [SerializeField] private Animator animator;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "L1_M1" /*|| SceneManager.GetActiveScene().name == "VFX Scene"*/)
            isFirstLevel = true;
        else
            isFirstLevel = false;

        if (!curse)
        {
            ghostParticles.Play();
        }

        if (pMovement == null)
        {
            pMovement = GetComponentInParent<PlayerMovement>();
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
        }
        else
        {
            this.gameObject.layer = 8;
            ghostParticles.Play();
        }

        AudioManager.Instance.LoadSFX(curseChangeClipName, curseChangeClip);
        AudioManager.Instance.PlaySFX(curseChangeClipName);
    }

    public void SetStopMovement(bool set)
    {
        pMovement.movementEnabled = set;
    }
}
