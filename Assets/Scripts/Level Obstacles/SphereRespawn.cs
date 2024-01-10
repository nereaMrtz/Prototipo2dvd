using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRespawn : MonoBehaviour
{
    private Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -50f)
        {
            transform.position = initPos;
        }
    }
}
