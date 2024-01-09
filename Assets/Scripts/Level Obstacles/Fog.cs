using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    Rigidbody rb;
    public bool active = false;
    [SerializeField] Vector3 movementVelocity;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (active)
        {
            rb.AddForce(movementVelocity*Time.deltaTime);
            if (movementVelocity.x < maxSpeed)
            {
                movementVelocity.x += (acceleration * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.layer == 8)
        {
            PlayerContoller aux = other.gameObject.GetComponent<PlayerContoller>();
            aux.ChangeMaldicion();
        }
    }
}
