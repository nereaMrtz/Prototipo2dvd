using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCinematicEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //AddElements.Instance.AddElement(other.gameObject);
        Destroy(other.gameObject);
    }
}
