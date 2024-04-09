using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    Vector2 movementInput;
    bool jumpInput;

    [Header("Movement")]
    public float topSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 30f;
    public float jumpForce = 1f;
    public float groundedRayLength = 0.1f;
    public float gravity = -10.0f;

    private Vector3 targetMovement = Vector3.zero;
    public GameObject groundCheck;
    public GameObject rightCheck;
    public GameObject leftCheck;

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



    // Update is called once per frame
    void Update()
    {
        movementInput.Normalize();       
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
            targetMovement.y += gravity * Time.fixedDeltaTime;
        else
            targetMovement.y = 0.0f;

        if (Mathf.Abs(movementInput.x) > 0.01f)
        {
            targetMovement.x = Mathf.Lerp(targetMovement.x, movementInput.x * topSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime * 40.0f); ;
        }
        else
            targetMovement.x = Mathf.Lerp(targetMovement.x, 0.0f, Time.fixedDeltaTime * 20.0f);

        if (isCollidingRight())
            targetMovement.x = Mathf.Min(targetMovement.x, 0.0f);
        if (isCollidingLeft())
            targetMovement.x = Mathf.Max(targetMovement.x, 0.0f);

        rb.MovePosition(rb.position + targetMovement);
       
        if (jumpInput && IsGrounded())
        {
            targetMovement.y = jumpForce;
        }
    }

    bool IsGrounded()
    {
        return Physics.OverlapSphere(groundCheck.transform.position, 0.2f).Length > 1;
    }

    bool isCollidingRight()
    {
        return Physics.OverlapSphere(rightCheck.transform.position, 0.2f).Length > 1;
    }

    bool isCollidingLeft()
    {
        return Physics.OverlapSphere(leftCheck.transform.position, 0.2f).Length > 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.2f);
    }
}
