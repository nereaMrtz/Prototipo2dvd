using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip releaseSound;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.clip = pressSound;
        audioSource.Play();
        Debug.Log("1");
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
        audioSource.clip = releaseSound;
        audioSource.Play();
    }

}
