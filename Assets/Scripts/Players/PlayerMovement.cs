using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private Vector2 movementInput;
    private bool jumpInput;

    [Header("Movement")]
    public float topSpeed = 10f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    private Vector3 targetMovement = Vector3.zero;

    [Header("Jump")]
    public float jumpForce = 1f;
    public float groundedRayLength = 0.1f;
    public float gravity = -10.0f;
    public GameObject groundCheck;
    public GameObject rightCheck;
    public GameObject leftCheck;

    [Header("Coyote Time")]
    [Tooltip("This marks the time the player will have to jump after it leaves contact with the ground")]
    public float coyoteTime = 0.5f;
    private float coyoteTimer;
    private bool inCoyote = false;
    private bool prevGrounded;

    [Header("InputBuffer")]
    [Tooltip("This is the time during which the player will jump as soon as it makes contat with the ground")]
    public float inputBuffer = 0.5f;
    private float inputBufferTimer;
    private bool inBuffer = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponentInParent<Rigidbody>();
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

    void Update()
    {
        movementInput.Normalize();
    }

    private void FixedUpdate()
    {
        #region Movement
        if (Mathf.Abs(movementInput.x) > 0.01f)
        {
            targetMovement.x = Mathf.Lerp(targetMovement.x, movementInput.x * topSpeed, Time.fixedDeltaTime * acceleration); ;
        }
        else
            targetMovement.x = Mathf.Lerp(targetMovement.x, 0.0f, Time.fixedDeltaTime * deceleration);

        #endregion

        #region Coyote Time

        if (prevGrounded && !IsGrounded())
        {
            inCoyote = true;
            coyoteTimer = coyoteTime;
        }

        if (inCoyote)
        {
            coyoteTimer -= Time.fixedDeltaTime;
            if (coyoteTimer <= 0.0f)
            {
                inCoyote = false;
            }
        }

        #endregion

        #region Input Buffer

        if (jumpInput && !IsGrounded())
        {
            inBuffer = true;
            inputBufferTimer = inputBuffer;
        }

        if (inBuffer)
        {
            inputBufferTimer -= Time.fixedDeltaTime;
            if (inputBufferTimer <= 0.0f)
            {
                inBuffer = false;
            }
        }

        #endregion

        #region Jump

        if ((jumpInput || inBuffer) && (IsGrounded() || inCoyote))
        {

            rb.AddForce(Vector3.up * jumpForce);
            inCoyote = false;
            inBuffer = false;
            jumpInput = false;
        }

        if (!(IsCollidingLeft() || IsCollidingRight()))
            targetMovement.y = rb.velocity.y;

        if (!IsGrounded())
        {
            float gravinc = gravity * Time.fixedDeltaTime;
            targetMovement.y += gravinc;
        }
        else
            targetMovement.y = 0.0f;

        #endregion

        rb.velocity = targetMovement;

        prevGrounded = IsGrounded();


    }

    bool IsGrounded()
    {
        return Physics.OverlapSphere(groundCheck.transform.position, 0.2f).Length > 1;
    }

    bool IsCollidingRight()
    {
        return Physics.OverlapSphere(rightCheck.transform.position, 0.2f).Length > 1;
    }
    bool IsCollidingLeft()
    {
        return Physics.OverlapSphere(leftCheck.transform.position, 0.2f).Length > 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.2f);
    }

}
