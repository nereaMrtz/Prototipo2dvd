using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHazard : MonoBehaviour
{
    PlayerContoller player1 = null;
    CharacterController player1Controller = null;

    PlayerContoller player2 = null;
    CharacterController player2Controller = null;

    [Header("Force")]
    [SerializeField] Vector3 force;

    // Update is called once per frame
    void Update()
    {
        if (player1 != null)
        {
            player1Controller.Move(force * Time.deltaTime);
        }
        if (player2 != null)
        {
            player2Controller.Move(force * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player1 == null)
            {
                player1 = other.GetComponent<PlayerContoller>();
                player1Controller = other.GetComponent<CharacterController>();
            }
            else if (player2 == null)
            {
                player2 = other.GetComponent<PlayerContoller>();
                player2Controller = other.GetComponent<CharacterController>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (player1 != null)
            {
                player1Controller.enabled = true;
                player1 = null;
                player1Controller = null;
            }
            else if (player2 != null)
            {
                player2Controller.enabled = true;
                player2 = null;
                player2Controller = null;
            }
        }
    }
}
