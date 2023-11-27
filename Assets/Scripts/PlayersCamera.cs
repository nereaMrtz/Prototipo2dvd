using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset = new Vector3(0, 3, -30);

    private void Update()
    {
        // Debug.Log(targets.Count);
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = newPosition;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
            // Debug.Log(targets[i].position);
        }
        return bounds.center;
    }

    public void AddPlayer(PlayerInput player)
    {
        Transform aux = player.gameObject.transform;

        targets.Add(aux);
    }
}
