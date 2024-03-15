using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRespawn : MonoBehaviour
{
    private Vector3 initPos;

    private Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;    
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -50f)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = initPos;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Electricity")
        {
            Respawn();
        }
    }
}
