 using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float Speed = 1.0f;
    private void Update()
    {
        this.gameObject.transform.Rotate(0, Speed * Time.deltaTime, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            CoinManager.Instance.AddCoin();

            audioSource.clip = coinSound;
            audioSource.Play();
            StartCoroutine(DestroyThis());
        }
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
