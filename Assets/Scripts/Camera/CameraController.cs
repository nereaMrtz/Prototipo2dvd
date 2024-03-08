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

    [SerializeField] private GameObject Ghost1;
    [SerializeField] private GameObject Ghost2;

    [SerializeField] private float timeToChange = 0.5f;
    private float oldY = 4.6f;

    private void Start()
    {
        oldY = target1.transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {     
        //Debug.Log(target1.transform.position.y + " " + target2.transform.position.y);
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
       
        float newY1 = Ghost1.transform.position.y + 3f;
        float newY2 = Ghost2.transform.position.y + 3f;

        // Move both targets to the new Y position
        target1.transform.DOMoveY(newY1, timeToChange);
        target2.transform.DOMoveY(newY2, timeToChange);
    }

    void SecondPosition()
    {
        float newY = oldY + 3f;
        target1.transform.DOMoveY(newY, timeToChange);
        target2.transform.DOMoveY(newY, timeToChange);
    }

    void ThirdPosition()
    {
        float newY = oldY + 5f;
        target1.transform.DOMoveY(newY, timeToChange);
        target2.transform.DOMoveY(newY, timeToChange);
    }
}
