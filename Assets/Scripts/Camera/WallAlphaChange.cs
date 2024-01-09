using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallAlphaChange : MonoBehaviour
{
    [SerializeField] GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wall.SetActive(true);
    }
}
