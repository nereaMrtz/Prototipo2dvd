using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.InputSystem.DefaultInputActions;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]

public class PlayerContoller : MonoBehaviour
{  
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private Vector2 movementInput = Vector2.zero;
    private bool hasJumped = false;
    private bool canWallJump = false;

    public bool maldicion;

    [SerializeField] PlayerContoller otherPlayer;

    [SerializeField] GameObject bolita;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        float aux = context.ReadValue<float>();
        if (aux != 0f ) { hasJumped = true; }
        else { hasJumped = false;  }
        // hasJumped = context.action.triggered;
    }

    public void OnMaldicion(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        { 
            if (otherPlayer.GetMaldicion() == false)
            {
                otherPlayer.SetMaldicion(true);
                maldicion = false;
            }
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(hasJumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        //WallJump
        if(hasJumped && canWallJump)
        {
           
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(maldicion);

        if(maldicion)
        {
            bolita.SetActive(true);
            Physics.IgnoreLayerCollision(3, 7, true);
            Physics.IgnoreLayerCollision(3, 6, true);
        }

        if(!maldicion)
        {
            bolita.SetActive(false);
            Physics.IgnoreLayerCollision(3, 7, false);
            Physics.IgnoreLayerCollision(3, 6, false);

        }

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

        if (hit.gameObject.layer == 6 && !maldicion)
        {
            Debug.Log("NOOchocando");
        }
        else 
        {
            Debug.Log("chocando");
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            body.velocity = pushDir * pushPower;

        }
        
    
        
        //Caja agua
        //if (hit.gameObject.layer == 6 || maldicion)
        //{
        //    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        //    body.velocity = pushDir * pushPower;
        //}
        //else{
        //    Physics.IgnoreLayerCollision(3, 6, false);
        //}
    }

    public bool GetMaldicion() { return maldicion; }
    public void SetMaldicion(bool maldicion) { this.maldicion = maldicion;}
}
