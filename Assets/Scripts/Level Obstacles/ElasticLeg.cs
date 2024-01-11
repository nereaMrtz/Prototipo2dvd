using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticLeg : MonoBehaviour
{
    [SerializeField] float jumpBoost;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().BoostJump(jumpBoost);
        }
    }
}
