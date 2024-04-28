using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]

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
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject rightCheck;
    [SerializeField] private GameObject leftCheck;

    [Header("Coyote Time")]
    [Tooltip("This marks the time the player will have to jump after it leaves contact with the ground")]
    public float coyoteTime = 0.5f;
    private float coyoteTimer;
    private bool inCoyote = false;
    private bool hasJumped = false;
    private bool prevGrounded;

    [Header("InputBuffer")]
    [Tooltip("This is the time during which the player will jump as soon as it makes contat with the ground")]
    public float inputBuffer = 0.5f;
    private float inputBufferTimer;
    private bool inBuffer = false;

    [Header("Modificators")]
    public bool movementEnabled = true;
    [HideInInspector] public bool curse;

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
        if (movementEnabled)
        {

            #region Gravity

            targetMovement.y = rb.velocity.y;

            if (!IsGrounded())
            {
                float gravinc = gravity * Time.fixedDeltaTime;
                targetMovement.y += gravinc;
            }
            else
            {
                targetMovement.y = 0.0f;
                hasJumped = false;
            }

            #endregion

            #region Movement

            if (Mathf.Abs(movementInput.x) > 0.01f)
            {
                targetMovement.x = Mathf.Lerp(targetMovement.x, movementInput.x * topSpeed, Time.fixedDeltaTime * acceleration); ;
            }
            else
                targetMovement.x = Mathf.Lerp(targetMovement.x, 0.0f, Time.fixedDeltaTime * deceleration);

            #endregion

            #region Coyote Time

            if (prevGrounded && !IsGrounded() && !hasJumped)
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

            if ((IsCollidingLeft() && movementInput.x < 0.0f) || (IsCollidingRight() && movementInput.x > 0.0f))
            {
                targetMovement.x = 0.0f;
            }


            if ((jumpInput || inBuffer) && (IsGrounded() || inCoyote))
            {
                if (inCoyote)
                    movementInput.y = 0.0f;
                rb.AddForce(Vector3.up * jumpForce);
                inCoyote = false;
                inBuffer = false;
                jumpInput = false;
                hasJumped = true;
            }



            #endregion

            rb.velocity = targetMovement;

            prevGrounded = IsGrounded();

        }

    }

    bool IsGrounded()
    {
        if (curse)
            return Physics.OverlapSphere(groundCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6 | 0 << 7, QueryTriggerInteraction.Ignore).Length > 1;
        else
            return Physics.OverlapSphere(groundCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1;
    }

    bool IsCollidingRight()
    {
        bool rtn;
        if (curse)
            rtn = Physics.OverlapSphere(rightCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1;
        else
            rtn = Physics.OverlapSphere(rightCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1;
        return rtn;
    }

    bool IsCollidingLeft()
    {
        bool rtn;
        if (curse)
            rtn = Physics.OverlapSphere(leftCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1;
        else
            rtn = Physics.OverlapSphere(leftCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1;
        return rtn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.2f);
    }

}
