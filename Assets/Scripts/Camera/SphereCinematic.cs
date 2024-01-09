using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCinematic : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sphere")
        {
            AddElements.Instance.AddElement(other.gameObject);
        }
    }
}
