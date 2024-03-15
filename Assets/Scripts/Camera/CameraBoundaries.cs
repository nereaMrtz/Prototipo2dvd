using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CameraBoundaries : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider1;
    [SerializeField] private BoxCollider boxCollider2;
    private Camera mainCamera;

    [SerializeField] private Transform cameraTarget; // The target the camera is looking at

    public bool stopScaling = false;
    void Start()
    {
        boxCollider1 = GetComponent<BoxCollider>();
        mainCamera = GetComponent<Camera>();

        if (boxCollider1 == null)
        {
            Debug.LogError("BoxCollider component not found.");
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogError("Camera component not found.");
            return;
        }

        if (cameraTarget == null)
        {
            Debug.LogError("Camera target not assigned.");
            return;
        }

        UpdateColliderSize();
    }

    void Update()
    {
        //// Update collider size if the camera's field of view changes
        if (mainCamera.orthographicSize <= 6f)
        {
            UpdateColliderSize();
        }
    }

    void UpdateColliderSize()
    {        

        // Calculate collider size based on the camera's field of view and distance to target
        float colliderSizeY = 2 * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * (cameraTarget.position - mainCamera.transform.position).magnitude;
        float colliderSizeX = colliderSizeY * mainCamera.aspect;


        // Set collider size and position for boxCollider1
        boxCollider1.size = new Vector3(1.2f, 100f, 5f);
        boxCollider1.center = new Vector3((colliderSizeX)/1.9f, 0f, 15f); // Adjust the Z position as needed

        // Set collider size and position for boxCollider2
        boxCollider2.size = new Vector3(1.2f, 100f, 5f);
        boxCollider2.center = new Vector3((-colliderSizeX)/ 1.9f, 0f, 15f); // Adjust the Z position as needed        
        

    }
}
