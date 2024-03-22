using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    Vector2 movementInput;
    bool jumpInput;

    [Header("Movement")]
    public float speed;

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

    }
}
