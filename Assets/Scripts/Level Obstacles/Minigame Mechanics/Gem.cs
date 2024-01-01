using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public bool collected { get; private set; }
    [SerializeField] bool forCursed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (forCursed && other.gameObject.GetComponent<PlayerContoller>().curse)
            {
                Collection(other.gameObject);
            }
            else if (!forCursed && !other.gameObject.GetComponent<PlayerContoller>().curse)
            {
                Collection(other.gameObject);
            }
        }
    }

    private void Collection(GameObject player) {
        collected = true;
    
    }
}
