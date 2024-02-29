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

    [SerializeField] private List<Camera> cameras = new List<Camera>();
    

    [SerializeField] private float timeToChange = 0.5f;

    [SerializeField] private Camera mainCamera;
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
            SetSplitScreen();
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

    void SetSplitScreen()
    {
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
    }

    void SetSingleCamera()
    {
        foreach(Camera cam in cameras) 
        {
            cam.enabled = false;
            cam.gameObject.SetActive(false);
        }
        mainCamera.enabled = true;
        //mainCamera.gameObject.SetActive(true);
        mainCamera.rect = new Rect(0, 0, 1, 1);
        //Camera cameraToSet = this.GetComponent<Camera>();
    }
}
