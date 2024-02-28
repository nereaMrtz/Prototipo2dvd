using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("puta");

        if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("puta2");
            CoinManager.Instance.AddCoin();
            Destroy(this.gameObject);
        }
    }
}
