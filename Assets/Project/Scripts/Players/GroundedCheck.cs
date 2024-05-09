using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(1);
        isGrounded = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        isGrounded = false;
    }
}
