using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeLimiter : MonoBehaviour
{    

    [SerializeField] private float minOrthographicSize = 2f;
    [SerializeField] private float maxOrthographicSize = 10f;

    void Update()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Example: Limit the orthographic size between minOrthographicSize and maxOrthographicSize
            float newOrthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minOrthographicSize, maxOrthographicSize);

            // Apply the new orthographic size to the main camera
            mainCamera.orthographicSize = newOrthographicSize;
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }
}

