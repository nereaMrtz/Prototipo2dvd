using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.InputSystem.DefaultInputActions;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]

public class PlayerContoller : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 2.0f;
    private CharacterController controller;
    private Vector2 movementInput = Vector2.zero;
    private Vector3 playerVelocity;

    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private bool jumpInput = false;
    private bool canWallJump = false;

    [SerializeField] private float maxCoyoteTime = 0.25f;
    private float coyoteTimer;
    private bool hasJumped = false;
    private bool wasGrounded;
    private bool inCoyote = false;

    [SerializeField] private float inputBufferTime = 0.25f;
    private float buffer;
    private bool inputBuffer;

    [Header("Maldición")]

    public bool maldicion;

    [SerializeField] PlayerContoller otherPlayer;

    // [SerializeField] Material ghost;
    [SerializeField] Material noMalditoMaterial;
    [SerializeField] Material malditoMaterial;

    private float distance;

    private AudioManager sound;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        sound = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        float aux = context.ReadValue<float>();
        if (aux != 0f)
        {
            jumpInput = true;
        }
        else { jumpInput = false; }
        // hasJumped = context.action.triggered;
    }

    public void OnMaldicion(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            if (otherPlayer.GetMaldicion() == false)
            {
                otherPlayer.SetMaldicion(true);
                otherPlayer.gameObject.layer = 9;
                maldicion = false;
                gameObject.layer = 8;

            }
            sound.maldicion.Play();
        }
    }

    // LAYERS
    // 8: NoMaldito
    // 9: Maldito

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (!groundedPlayer && wasGrounded)
        {
            inCoyote = true;
            coyoteTimer = maxCoyoteTime;
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            hasJumped = false;
        }


        distance = otherPlayer.transform.position.x - gameObject.transform.position.x;

        //CALCULO DISTANCIAS PLAYERS
        if (distance > 25)
        {

        }

        Vector3 move = new Vector3(movementInput.x, -0.5f, .0f);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (jumpInput)
        {
            if (groundedPlayer)
            {
                Jump();
            }
            else if (inCoyote && !hasJumped)
            {
                Jump();
                inCoyote = false;
            }
            else
            {
                inputBuffer = true;
                buffer = inputBufferTime;
            }
        }

        if (inputBuffer)
        {
            if (groundedPlayer)
                Jump();
            buffer -= Time.deltaTime;
            if(buffer < 0)
            {
                inputBuffer = false;
            }
            
        }

        //WallJump
        if (hasJumped && canWallJump)
        {

        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Physics.IgnoreLayerCollision(9, 7, false); // Layer 9: Maldito
        Physics.IgnoreLayerCollision(8, 7, true); // Layer 8: NoMaldito

        if (maldicion)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = malditoMaterial;
        }

        if (!maldicion)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = noMalditoMaterial;
        }

        if (inCoyote)
        {
            coyoteTimer -= Time.deltaTime;

            if (coyoteTimer < 0)
                inCoyote = false;
        }

        wasGrounded = groundedPlayer;

    }

    void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        sound.jump.Play();
        hasJumped = true;
        inputBuffer = false;
    }

    // this script pushes all rigidbodies that the character touches
    float pushPower = 2.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, -0.5f);

        body.velocity = pushDir * pushPower;

    }

    public bool GetMaldicion() { return maldicion; }
    public void SetMaldicion(bool maldicion) { this.maldicion = maldicion; }
}