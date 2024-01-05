using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCinematic : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        AddElements.Instance.AddElement(other.gameObject);
    }
}
