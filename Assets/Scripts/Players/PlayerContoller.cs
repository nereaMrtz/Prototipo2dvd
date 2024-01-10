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



[RequireComponent(typeof(CharacterController))]

public class PlayerContoller : MonoBehaviour
{
    [Header("Player Movement")]

    [SerializeField] private float playerSpeed = 2.0f;
    private CharacterController controller;
    private Vector2 movementInput = Vector2.zero;
    private Vector3 playerVelocity;
    private Vector3 lastInput = Vector3.right;

    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private bool jumpInput = false;
    public bool interactInput { get; private set; }

    [SerializeField] private float maxCoyoteTime = 0.25f;
    private float coyoteTimer;
    private bool hasJumped = false;
    private bool wasGrounded;
    private bool inCoyote = false;

    [SerializeField] private float inputBufferTime = 0.25f;
    private float buffer;
    private bool inputBuffer;

    bool jumpBoosted=false;
    float jumpBoost;

    float pushPower = 2.0f;
    float pushPlayerPower = 1.0f;
    Vector3 impact;

    private bool stopMovement = false;


    [Header("Curse")]

    [SerializeField] PlayerContoller otherPlayer;
    public bool curse;

    [SerializeField] Material normalMat;
    [SerializeField] Material cursedMat;

    [HideInInspector]public bool isFirstLevel = false;

    private float distance;

    private AudioManager sound;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "L1_M1")        
            isFirstLevel = true;        
        else        
            isFirstLevel= false;        
        controller = this.gameObject.GetComponent<CharacterController>();
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
            sound.maldicion.Play();
        }
    }

    // LAYERS
    // 8: Not Cursed
    // 9: Cursed

    void Update()
    {
        if (stopMovement)
        {
            return;
        }
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

        Vector3 move = new Vector3(movementInput.x, 0f, 0f);
        controller.Move(move * Time.deltaTime * playerSpeed);
        //controller.enabled = false;
        //transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        //controller.enabled = true;

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
            if (buffer < 0)
            {
                inputBuffer = false;
            }

        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        if(move != Vector3.zero)
        {
            lastInput = move;
        }        
        controller.transform.forward = lastInput; 



        Physics.IgnoreLayerCollision(9, 7, false); // Layer 9: Cursed
        Physics.IgnoreLayerCollision(8, 7, true); // Layer 8: Not Cursed

        if (curse)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = cursedMat;
        }

        if (!curse)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = normalMat;
        }

        if (inCoyote)
        {
            coyoteTimer -= Time.deltaTime;

            if (coyoteTimer < 0)
                inCoyote = false;
        }

        wasGrounded = groundedPlayer;
        if (transform.position.z != -0.5f)
        {
            controller.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            controller.enabled = true;
        }
    }

    void Jump()
    {
        if (jumpBoosted)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue * jumpBoost);
            jumpBoosted = false;
        }
        else
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        sound.jump.Play();
        hasJumped = true;
        inputBuffer = false;
    }

    //This script pushes all rigidbodies that the character touches
    void OnControllerColliderHit(ControllerColliderHit hit)
    {        
        
        if (hit.gameObject.GetComponent<PlayerContoller>() == otherPlayer)
        {       
            impact = AddImpact(hit.moveDirection, hit.moveLength * pushPlayerPower);
            // apply the impact force:
            if (impact.magnitude > 0.2) otherPlayer.GetComponent<CharacterController>().Move(impact * Time.deltaTime);
            // consumes the impact energy each cycle:            
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }

        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        //We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, 0f);

        body.velocity = pushDir * pushPower;

        SetStopMovement(false);

    }

    [Tooltip("This changes the curse and layers that it collides with")]
    public void ChangeMaldicion()
    {
        if(isFirstLevel) 
        {
            return;
        }
        curse = !curse;
        if (curse)
            this.gameObject.layer = 9;
        else
            this.gameObject.layer = 8;
    }

    Vector3 AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        return impact + dir.normalized * force;
    }

    public void SetStopMovement(bool set)
    {
        stopMovement = set;
    }

    public void BoostJump(float boost)
    {
        jumpBoosted = true;
        jumpBoost = boost;
    }

    public void FreezePosition()

    {

        Debug.Log("freezzeandooo");

        this.gameObject.GetComponent<PlayerContoller>().enabled = false;

        otherPlayer.enabled = false;

    }

    public void UnfreezePosition()

    {

        this.gameObject.GetComponent<PlayerContoller>().enabled = true;

        otherPlayer.enabled = true;

    }

    CharacterController GetContoller()

    {

        return controller;

    }
}