using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirHazard : MonoBehaviour
{
    PlayerController player1 = null;
    PlayerController player2 = null;

    [Header("Force")]
    [SerializeField] Vector3 force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            player1 = other.GetComponent<PlayerController>();
            player1.pMovement.SetForceModifier(force);
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            player2 = other.GetComponent<PlayerController>();
            player2.pMovement.SetForceModifier(force);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player1"))
        {
            player1.pMovement.SetForceModifier(Vector3.zero);
            player1 = null;

        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            player2.pMovement.SetForceModifier(Vector3.zero);
            player2 = null;
        }
    }
}
