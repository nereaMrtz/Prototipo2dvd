using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    private void OnTriggerEnter(Collider other)
    {
        //Adding Sounds
    }

    private void OnTriggerStay(Collider other)
    {
        foreach ( GameObject platform in platforms)
        {
            platform.GetComponent<MovingPlatform>().Interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject platform in platforms)
        {
            platform.GetComponent<MovingPlatform>().Uninteract();
        }
    }

}
