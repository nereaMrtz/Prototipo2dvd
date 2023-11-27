using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersCamera : MonoBehaviour
{
    [SerializeField] Transform ghost1;
    [SerializeField] Transform ghost2;

   // [SerializeField] Vector3 minSize;
    //[SerializeField] Vector3 maxSize;

    public Vector3 offset = new Vector3(0, 3, -30);

    private void Update()
    {
       // Debug.Log(targets.Count);
    }

    private void LateUpdate()
    {

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = newPosition;
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(ghost1.position, Vector3.zero);
        //var bounds = new Bounds();
       // bounds.SetMinMax(minSize, maxSize);
 
        //bounds.Encapsulate(ghost1.position);
        bounds.Encapsulate(ghost2.position);

       // bounds.max = maxSize;
        return bounds.center;
    }

}
