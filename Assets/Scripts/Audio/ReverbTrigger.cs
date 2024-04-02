using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbTrigger : MonoBehaviour
{
    [SerializeField] float reverb = 0.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            AudioManager.Instance.SetReverb(reverb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            AudioManager.Instance.SetReverb(0.0f);
        }
    }
}

