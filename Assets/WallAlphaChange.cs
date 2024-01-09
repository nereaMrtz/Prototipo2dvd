using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallAlphaChange : MonoBehaviour
{
    [SerializeField] float lerpTime;
    float timer;
    [SerializeField] float minAlpha;
    float maxAlpha = 255;
    bool playerIn = false;
    Color tmp;

    private void Start()
    {
        tmp = this.GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = true;
            timer = lerpTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = false;
            timer = lerpTime;
        }
    }

    private void Update()
    {
        if (playerIn)
        {
            tmp.a = Mathf.Lerp(minAlpha, 255, timer);
            Debug.Log(tmp.a);
            this.GetComponent<MeshRenderer>().material.color = tmp;
        }
        else 
        {
            tmp.a = Mathf.Lerp(255, minAlpha, timer);
            Debug.Log(tmp.a);
            this.GetComponent<MeshRenderer>().material.color = tmp;
        }
        if (timer < .0f)
            timer -= Time.deltaTime;

    }
}
