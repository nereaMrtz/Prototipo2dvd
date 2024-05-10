using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireButton : MonoBehaviour
{
    [SerializeField] GameObject[] walls;

    [SerializeField] GameObject pressButton;    

    bool wallActive=true;
    float timer;
    public float wallDelay;

    private GameObject ghost1;
    private GameObject ghost2;
    void Start()
    {
        ghost1 = GameObject.FindGameObjectWithTag("Player1");
        ghost2 = GameObject.FindGameObjectWithTag("Player2");

        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {    
            pressButton.SetActive(true);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        timer = 0.0f;
        wallActive = false;
        pressButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!wallActive && timer < wallDelay)
        {
            timer += Time.deltaTime;
        }
        else if (!wallActive)
        {
            wallActive=true;
            foreach (var wall in walls)
            {
                wall.SetActive(true);
            }
        }

        if(ghost1.GetComponent<PlayerController>().interactInput == true && pressButton.activeInHierarchy || ghost2.GetComponent<PlayerController>().interactInput == true && pressButton.activeInHierarchy)
        {
            foreach (var wall in walls)
            {
                wall.SetActive(false);
            }
        }
    }
}
