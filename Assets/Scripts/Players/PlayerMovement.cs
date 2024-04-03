using System.Collections;
using System.Collections.Generic;
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
        if (movementInput != Vector2.zero)
        {
            Vector2 currentvelocity = rb.velocity;

            Vector2 targetVelocity = movementInput * topSpeed;

            Vector2 deltaVelocity = targetVelocity - currentvelocity;

            Vector2 accelVector = deltaVelocity.normalized * (acceleration * Time.fixedDeltaTime);

            if (accelVector.sqrMagnitude > deltaVelocity.sqrMagnitude)
            {
                accelVector = deltaVelocity;
            }
            rb.velocity = new Vector3(rb.velocity.x + accelVector.x, rb.velocity.y, 0.0f);


            Debug.Log(rb.velocity);
        }
        else
        {
            Vector2 decelVector = -rb.velocity.normalized * (deceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector3(rb.velocity.x + decelVector.x, rb.velocity.y, 0.0f); 


            if (Vector2.Dot((Vector2)(rb.velocity) + decelVector, decelVector) > 0f)
            {
                rb.velocity = Vector3.zero;
            }
        }

        if (jumpInput)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpForce * -3.0f * Physics.gravity.y), 0.0f);

        }
        else if(!IsGrounded())
        {
            rb.AddForce(new Vector3(0.0f, Physics.gravity.y * 5, 0.0f));
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
        }
    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.green, groundedRayLength);
        return Physics.Raycast(transform.position, -Vector3.up, groundedRayLength);
    }
}
