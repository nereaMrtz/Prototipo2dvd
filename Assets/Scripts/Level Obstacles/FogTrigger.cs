using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    [SerializeField] Fog fog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fog.gameObject.SetActive(true);
            fog.active = true;
        }
    }
}
