using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fog : MonoBehaviour
{
    Rigidbody rb;
    public bool active = false;
    [SerializeField] Vector3 movementVelocity;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] AudioSource audioSource;
    bool cursed = true;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        audioSource.loop = true;
        audioSource.Play();

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
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            PlayerController aux = other.gameObject.GetComponent<PlayerController>();
            aux.isFirstLevel = false;
            if (cursed)
            {
                aux.ChangeMaldicion();
                cursed = false;
            }
            else
            {
                SceneManager.LoadScene("L2_M1");
            }
        }
    }
}