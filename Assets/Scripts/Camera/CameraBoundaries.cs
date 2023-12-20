using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Camera mainCamera;

    [SerializeField] private Transform cameraTarget; // The target the camera is looking at

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        mainCamera = GetComponent<Camera>();

        if (boxCollider == null)
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
        // Update collider size if the camera's field of view changes
        if (Mathf.Approximately(boxCollider.size.x, mainCamera.fieldOfView))
        {
            UpdateColliderSize();
        }
    }

    void UpdateColliderSize()
    {
        // Calculate collider size based on the camera's field of view and distance to target
        float colliderSizeY = 2 * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * (cameraTarget.position - mainCamera.transform.position).magnitude;
        float colliderSizeX = colliderSizeY * mainCamera.aspect;

        boxCollider.size = new Vector3(colliderSizeX, colliderSizeY, 4f);
        boxCollider.center = new Vector3(0f, 0f, 10f);
    }

}
