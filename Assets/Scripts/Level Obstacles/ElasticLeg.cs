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
            Debug.Log(1);
            other.gameObject.GetComponent<PlayerContoller>().BoostJump(jumpBoost);
        }
    }
}
