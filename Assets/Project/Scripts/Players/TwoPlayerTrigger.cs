using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoPlayerTrigger : MonoBehaviour
{
    bool ghost1 = false;
    bool ghost2 = false;

    [SerializeField] public UnityEvent EndingFunctions;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player1"))
            ghost1 = true;
        if(other.CompareTag("Player2"))
            ghost2 = true;
    }

    private void Update()
    {
        if (ghost1 && ghost2)
        {
            EndingFunctions.Invoke();
        }
    }
}
