using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider1;
    [SerializeField] private BoxCollider boxCollider2;
    private Camera mainCamera;

    [SerializeField] private Transform cameraTarget; // The target the camera is looking at

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
        //if (Mathf.Approximately(boxCollider1.size.x, mainCamera.fieldOfView))
        //{
        //}
        UpdateColliderSize();
    }

    void UpdateColliderSize()
    {
        // Calculate collider size based on the camera's field of view and distance to target
        float colliderSizeY = 2 * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * (cameraTarget.position - mainCamera.transform.position).magnitude;
        float colliderSizeX = colliderSizeY * mainCamera.aspect;

        // Set collider size and position for boxCollider1
        boxCollider1.size = new Vector3(0.2f, 3.5f, 5f);
        boxCollider1.center = new Vector3(colliderSizeX / 2f, 0f, 10f); // Adjust the Z position as needed

        // Set collider size and position for boxCollider2
        boxCollider2.size = new Vector3(0.2f, 3.5f, 5f);
        boxCollider2.center = new Vector3(-colliderSizeX / 2f, 0f, 10f); // Adjust the Z position as needed


    }
}
