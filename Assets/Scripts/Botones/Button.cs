using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private MovingPlatform platforms[];

    private void OnTriggerEnter(Collider other)
    {
        //Adding Sounds
    }

    private void OnTriggerStay(Collider other)
    {
        foreach ( MovingPlatform platform in platforms)
        {
            platform.Interact():
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (MovingPlatform platform in platforms)
        {
            platform.Uninteract():
        }
    }

}
