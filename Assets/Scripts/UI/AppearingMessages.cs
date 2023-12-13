using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingMessages : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canvas.gameObject.SetActive(false); ;
    }
}
