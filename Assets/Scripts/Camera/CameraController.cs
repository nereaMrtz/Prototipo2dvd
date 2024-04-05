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

    private GameObject arrow1;
    private GameObject arrow2;

     private List<Camera> cameras = new List<Camera>();
    

    private float timeToChange = 0.5f;

    private Camera mainCamera;

    private float oldY = 4.6f;

    private bool split = false;

    private void Start()
    {        
        oldY = target1.transform.position.y;
        arrow1.SetActive(false);
        arrow2.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        Debug.Log("Los fantasmas estan a una distancia de: " + Vector3.Distance(target1.transform.position, target2.transform.position));
        /*
        //Debug.Log(target1.transform.position.y + " " + target2.transform.position.y);
        if(Vector3.Distance(target1.transform.position, target2.transform.position) >= 18f)
        {         
            if(target1.transform.position.x < target2.transform.position.x)
                SetSplitScreen1();  
            else
                SetSplitScreen2();
        }
        else
        {
            SetSingleCamera();
        }
        /*else if(Vector3.Distance(target1.transform.position, target2.transform.position) >= 10f)
        {           
            SecondPosition();
        }
        else
        {
            FirstPosition();
        }
        */
    }

    void SetSplitScreen1()
    {
        if(!split)
        {
            Fade.Instance.FadeOut();            
            split = true;
        }
        mainCamera.enabled = false;
        //mainCamera.gameObject.SetActive(false);
        int cameraAmmount = cameras.Count;
        float x = -0.5f;
        for(int i = 0; i < cameraAmmount; i++) 
        {
            cameras[i].enabled = true;
            cameras[i].gameObject.SetActive(true);
            //Set it split
            float auxX = x;
            float y = 0.0f;
            float width = 1;
            float height = 1;
            cameras[i].rect = new Rect(auxX, y, width, height);
            x *= -1;
        }
        arrow1.SetActive(true);
        arrow2.SetActive(true);
        //if (!split)
        //{
        //    Fade.Instance.FadeIn();
        //}
    }
    void SetSplitScreen2()
    {
        if (!split)
        {
            Fade.Instance.FadeOut();
            split = true;
        }
        mainCamera.enabled = false;
        //mainCamera.gameObject.SetActive(false);
        int cameraAmmount = cameras.Count;
        float x = -0.5f;
        for (int i = cameraAmmount-1; i >= 0; i--)
        {
            cameras[i].enabled = true;
            cameras[i].gameObject.SetActive(true);
            //Set it split
            float auxX = x;
            float y = 0.0f;
            float width = 1;
            float height = 1;
            cameras[i].rect = new Rect(auxX, y, width, height);
            x *= -1;
        }
        arrow1.SetActive(true);
        arrow2.SetActive(true);
        //if (!split)
        //{
        //    Fade.Instance.FadeIn();
        //}
    }
    void SetSingleCamera()
    {
        if(split) 
        {
            Fade.Instance.FadeOut();
            split = false;
        }
        foreach(Camera cam in cameras) 
        {
            cam.enabled = false;
            cam.gameObject.SetActive(false);
        }
        mainCamera.enabled = true;
        //mainCamera.gameObject.SetActive(true);
        mainCamera.rect = new Rect(0, 0, 1, 1);
        //Camera cameraToSet = this.GetComponent<Camera>();
        arrow1.SetActive(false);
        arrow2.SetActive(false);
    }
}
