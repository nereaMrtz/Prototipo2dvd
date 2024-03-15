using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    public static CheckPointMaster Instance { get; private set; }

    private Vector3 lastCheckPointPos;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one CheckPointMaster!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
            Instance = this;            
        
    }    

    public void SetLastCheckPointPos(Vector3 lastCheckPointPos)
    {
        this.lastCheckPointPos = lastCheckPointPos;
    }

    public Vector3 GetLastCheckPointPos()
    {
        return this.lastCheckPointPos;
    }
}
