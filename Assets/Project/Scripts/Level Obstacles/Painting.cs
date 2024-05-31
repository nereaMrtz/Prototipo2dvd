using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField] GameObject drop;
    bool dropped = false;

    bool shaken = false;
    bool shaking = false;
    [SerializeField] float shakeSpeed;
    [SerializeField] float shakeAmount;
    [SerializeField] float shakeDuration;
    float shakeTimer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!shaken)
            {
                Debug.Log(1);
                shaking = true;
                shakeTimer = shakeDuration;
            }
            else if(shaken && !dropped)
            {
                Debug.Log(2);
                Instantiate(drop);
            }
        }
    }

    private void Update()
    {
        if (shaking && shakeTimer > .0f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
        else if (shaking && shakeTimer <= .0f)
        {
            Debug.Log(false);
            shaking = false; 
            shaken = true;
        }
    }
}
