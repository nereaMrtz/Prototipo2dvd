using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
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
    private Vector3 forceModifier;

    [Header("Jump")]
    public float jumpForce = 1f;
    public float groundedRayLength = 0.1f;
    public float gravity = -10.0f;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject rightUpCheck;
    [SerializeField] private GameObject rightDownCheck;
    [SerializeField] private GameObject leftUpCheck;
    [SerializeField] private GameObject leftDownCheck;

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

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject armature;
    private Vector3 lastDirection = Vector3.forward;


    private float distance = 33.0f;
    PlayerController pController;

    void Start()
    {
        if (rb == null)
            rb = GetComponentInParent<Rigidbody>();
        if (curse)
        {
            groundCheck.layer = 9;
            leftUpCheck.layer = 9;
            leftDownCheck.layer = 9;
            rightUpCheck.layer = 9;
            rightDownCheck.layer = 9;
            this.gameObject.layer = 9;
        }
        else
        {
            groundCheck.layer = 8;
            leftUpCheck.layer = 8;
            leftDownCheck.layer = 8;
            rightUpCheck.layer = 8;
            rightDownCheck.layer = 8;
            this.gameObject.layer = 8;
        }
        if (pController == null)
            pController = GetComponent<PlayerController>();
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
        if (curse)
        {
            groundCheck.layer = 9;
            leftUpCheck.layer = 9;
            leftDownCheck.layer = 9;
            rightUpCheck.layer = 9;
            rightDownCheck.layer = 9;
        }
        else
        {
            groundCheck.layer = 8;
            leftUpCheck.layer = 8;
            leftDownCheck.layer = 8;
            rightUpCheck.layer = 8;
            rightDownCheck.layer = 8;
            this.gameObject.layer = 8;
        }
    }

    private void FixedUpdate()
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
            targetMovement.x = Mathf.Lerp(targetMovement.x, movementInput.x * topSpeed, Time.fixedDeltaTime * acceleration);
            animator.SetBool("IsWalking", true);
            if (movementInput.x < 0.0f)
                lastDirection = Vector3.forward;
            else if (movementInput.x > 0.0f)
                lastDirection = Vector3.back;
        }
        else
        {

            targetMovement.x = Mathf.Lerp(targetMovement.x, 0.0f, Time.fixedDeltaTime * deceleration);
            animator.SetBool("IsWalking", false);
        }

        armature.transform.forward = lastDirection;

        if (((IsCollidingLeft() && movementInput.x < 0.0f) || (IsCollidingRight() && movementInput.x > 0.0f)) && !IsGrounded())
        {
            targetMovement.x = 0.0f;
        }
        if (CheckDistanceLeft())
        {
            if (movementInput.x < 0.0f)
                targetMovement.x = 0.0f;
        }
        if (CheckDistanceRight())
        {
            if (movementInput.x > 0.0f)
                targetMovement.x = 0.0f;
        }

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

        if ((jumpInput || inBuffer) && (IsGrounded() || inCoyote))
        {
            if (inCoyote)
                movementInput.y = 0.0f;
            rb.AddForce(Vector3.up * jumpForce);
            inCoyote = false;
            inBuffer = false;
            jumpInput = false;
            hasJumped = true;
            animator.SetTrigger("Jump");
        }



        #endregion

        if (!movementEnabled)
        {
            targetMovement = Vector3.zero;
        }

        targetMovement += forceModifier;

        rb.velocity = targetMovement;

        prevGrounded = IsGrounded();
        animator.SetBool("IsGrounded", IsGrounded());

    }

    bool IsGrounded()
    {
        if (curse)
            return Physics.OverlapSphere(groundCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1;
        else
            return Physics.OverlapSphere(groundCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1;
    }

    bool IsCollidingRight()
    {
        bool rtn;
        if (curse)
            rtn = (Physics.OverlapSphere(rightUpCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1 ||
                Physics.OverlapSphere(rightDownCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1);
        else
            rtn = (Physics.OverlapSphere(rightUpCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1 ||
                Physics.OverlapSphere(rightDownCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1);
        return rtn;
    }

    bool IsCollidingLeft()
    {
        bool rtn;
        if (curse)
            rtn = (Physics.OverlapSphere(leftUpCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1 ||
                Physics.OverlapSphere(leftDownCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 0 << 6 | 1 << 7, QueryTriggerInteraction.Ignore).Length > 1);
        else
            rtn = (Physics.OverlapSphere(leftUpCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1 ||
                Physics.OverlapSphere(leftDownCheck.transform.position, 0.2f, 1 << 0 | 1 << 8 | 1 << 9 | 1 << 6, QueryTriggerInteraction.Ignore).Length > 1);
        return rtn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.2f);
    }

    [Tooltip("Returns false if it isn't in the limit of the camera")]
    bool CheckDistanceRight()
    {
        if (transform.position.x < pController.otherPlayer.transform.position.x)
        {
            return false;
        }
        else if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(pController.otherPlayer.transform.position.x)) < distance)
            return false;
        else
            return true;
    }

    bool CheckDistanceLeft()
    {
        if (transform.position.x > pController.otherPlayer.transform.position.x)
        {
            return false;
        }
        else if (Mathf.Abs(Mathf.Abs(pController.otherPlayer.transform.position.x) - MathF.Abs(transform.position.x)) < distance)
            return false;
        else
            return true;
    }

    public void SetForceModifier(Vector3 force)
    {
        forceModifier = force;
    }
}
