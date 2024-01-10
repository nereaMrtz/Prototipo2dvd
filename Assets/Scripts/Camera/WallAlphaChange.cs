using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class WallAlphaChange : MonoBehaviour
{
    [SerializeField] GameObject wall;
    private Color originalColor;
    private Color targetColor;

    private void Start()
    {
        originalColor = wall.GetComponent<MeshRenderer>().material.color;
        targetColor = originalColor; 
        targetColor.a = 0.3f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(false); 
           
            //wall.GetComponent<MeshRenderer>().material.DOColor(targetColor,0.5f);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        wall.SetActive(true);
        //wall.GetComponent<MeshRenderer>().material.DOColor(originalColor, 0.5f);
    }
}
