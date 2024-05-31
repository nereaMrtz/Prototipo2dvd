using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class IsInCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Plane[] cameraFrustrum;
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject aux = GameObject.FindGameObjectWithTag("MainCamera");
        cam = aux.GetComponent<Camera>();        
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cam != null) 
        {
            cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(cam);
        }
        else
        {
            GameObject aux = GameObject.FindGameObjectWithTag("MainCamera");
            cam = aux.GetComponent<Camera>();
        }
    }

    public bool IsInCameraNow()
    {
        var bounds = collider.bounds;
        return GeometryUtility.TestPlanesAABB(cameraFrustrum, bounds);
    }
}
