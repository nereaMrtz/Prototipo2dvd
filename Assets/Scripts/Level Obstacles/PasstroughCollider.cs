using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasstroughCollider : MonoBehaviour
{
    [SerializeField] GameObject collider;
    [SerializeField] GameObject TopCollider1;
    [SerializeField] GameObject TopCollider2;
    void Start()
    {
        collider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            collider.SetActive(true);
            other.transform.localScale = new Vector3(2f, 1.6f, 2f);
            TopCollider1.SetActive(false);

        }
        else if (other.CompareTag("Player2"))
        {
            collider.SetActive(true);
            other.transform.localScale = new Vector3(2f, 1.6f, 2f);
            TopCollider2.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        collider.SetActive(false);
        other.transform.localScale = new Vector3(2f, 2f, 2f);

    }
}

