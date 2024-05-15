using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 initialPos;

    [SerializeField] private float fallDelay, respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
       
    private void OnTriggerEnter(Collider coll)
    {

        if(coll.gameObject.tag == "Player1" || coll.gameObject.tag == "Player2")
        {            
            StartCoroutine("PlatformDrop");
        }
    }

    IEnumerator PlatformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        yield return new WaitForSeconds(respawnTime);
        ResetPosition();
    }

    private void ResetPosition()
    {
        rb.isKinematic = true;
        transform.position = initialPos;
    }
}
