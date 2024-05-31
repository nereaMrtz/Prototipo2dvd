using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
   [SerializeField] GameObject box;
    Vector3 checkpoint;

    private void Start()
    {
        checkpoint=box.transform.position;
    }

    public void Respawn()
    {
        box.transform.position = checkpoint;
        box.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
