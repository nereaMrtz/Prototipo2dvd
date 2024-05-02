using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointMaster : MonoBehaviour
{
    public static CheckPointMaster Instance { get; private set; }

    private Vector3 lastCheckPointPosGhost1;
    private Vector3 lastCheckPointPosGhost2;
    [SerializeField] private Vector3 initialPosGhost1;
    [SerializeField] private Vector3 initialPosGhost2;

    private void Start()
    {
        GameObject ghost1 = GameObject.FindGameObjectWithTag("Player1");
        if (ghost1 != null)
        {
            this.lastCheckPointPosGhost1 = initialPosGhost1;
            ghost1.GetComponent<Rigidbody>().position = initialPosGhost1;
            ghost1.GetComponent<PlayerMovement>().enabled = false;
            ghost1.transform.position = initialPosGhost1;
            ghost1.GetComponent<PlayerMovement>().enabled = true;
            AddElements.Instance.AddGhost(ghost1);
        }

        GameObject ghost2 = GameObject.FindGameObjectWithTag("Player2");
        if (ghost2 != null)
        {
            this.lastCheckPointPosGhost2 = initialPosGhost2;
            ghost2.GetComponent<PlayerMovement>().enabled = false;
            ghost2.GetComponent<Rigidbody>().position = initialPosGhost2;
            ghost2.transform.position = initialPosGhost2;
            ghost2.GetComponent<PlayerMovement>().enabled = true;
            AddElements.Instance.AddGhost(ghost2);
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
