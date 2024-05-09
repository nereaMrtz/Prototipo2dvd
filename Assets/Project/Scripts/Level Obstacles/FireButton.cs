using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireButton : MonoBehaviour
{
    [SerializeField] GameObject[] walls;

    bool wallActive=true;
    float timer;
    public float wallDelay;
    void Start()
    {
        foreach(GameObject wall in walls)
        {
            wall.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            foreach (var wall in walls)
            {
                wall.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0.0f;
        wallActive = false;
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
    }
}
