using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]private Vector3 firstPosition;
    [SerializeField]private Vector3 lastPosition;

    public void Interact()
    {
        if(transform.position != lastPosition) 
        {
            
        }
    }

    public void Uninteract()
    {
        
    }
}
