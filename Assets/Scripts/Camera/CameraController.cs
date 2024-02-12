using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private GameObject target1;
    [SerializeField]private GameObject target2;

    [SerializeField] private float timeToChange = 0.5f;
    private float oldY = 4.6f;

   
    // Update is called once per frame
    void Update()
    {     
        if(Vector3.Distance(target1.transform.position, target2.transform.position) >= 18f)
        {           
            ThirdPosition();
        }
        else if(Vector3.Distance(target1.transform.position, target2.transform.position) >= 10f)
        {            
            SecondPosition();
        }
        else
        {
            FirstPosition();
        }
    }

    void FirstPosition()
    {       
        target1.transform.DOMoveY(oldY, timeToChange);
        target2.transform.DOMoveY(oldY, timeToChange);
    }

    void SecondPosition()
    {
        float newY =  7f;
        target1.transform.DOMoveY(newY, timeToChange);
        target2.transform.DOMoveY(newY, timeToChange);
    }

    void ThirdPosition()
    {
        float newY = 9f;
        target1.transform.DOMoveY(newY, timeToChange);
        target2.transform.DOMoveY(newY, timeToChange);
    }
}
