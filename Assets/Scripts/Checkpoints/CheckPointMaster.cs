using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointMaster : MonoBehaviour
{
    public static CheckPointMaster Instance { get; private set; }

    private Vector3 lastCheckPointPosGhost1;
    private Vector3 lastCheckPointPosGhost2;
    [SerializeField] private Transform initialPosGhost1;
    [SerializeField] private Transform initialPosGhost2;

    private void Start()
    {
        GameObject ghost1 = GameObject.FindGameObjectWithTag("Player1");
        if (ghost1 != null)
        {
            this.lastCheckPointPosGhost1 = initialPosGhost1.position;
            ghost1.GetComponent<PlayerMovement>().enabled = false;
            ghost1.transform.position = initialPosGhost1.position;
        }
        GameObject ghost2 = GameObject.FindGameObjectWithTag("Player2");
        if (ghost2 != null)
        {
            this.lastCheckPointPosGhost2 = initialPosGhost2.position;
            ghost2.GetComponent<PlayerMovement>().enabled = false;
            ghost2.transform.position = initialPosGhost2.position;
        }
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
