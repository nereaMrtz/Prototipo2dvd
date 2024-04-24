using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMaster : MonoBehaviour
{
    public static CheckPointMaster Instance { get; private set; }

    private Vector3 lastCheckPointPosGhost1;
    private Vector3 lastCheckPointPosGhost2;

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

    public void SetLastCheckPointPosGhost1(Vector3 lastCheckPointPos)
    {
        Debug.Log("Ghost 1 checkpoint setted at" + lastCheckPointPos);
        this.lastCheckPointPosGhost1 = lastCheckPointPos;
    }
    public void SetLastCheckPointPosGhost2(Vector3 lastCheckPointPos)
    {
        Debug.Log("Ghost 2 checkpoint setted at" + lastCheckPointPos);
        this.lastCheckPointPosGhost2 = lastCheckPointPos;
    }

    public Vector3 GetLastCheckPointPosGhost1()
    {
        return this.lastCheckPointPosGhost1;
    }

    public Vector3 GetLastCheckPointPosGhost2()
    {
        return this.lastCheckPointPosGhost2;
    }
}
