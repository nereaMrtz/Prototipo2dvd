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
    private GameObject ghost1;
    private GameObject ghost2;

    bool activeCheckpoint1 = false;
    bool activeCheckpoint2 = false;

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
    private void Start()
    {
        ghost1 = GameObject.FindGameObjectWithTag("Player1");
        if (ghost1 != null)
        {
            this.lastCheckPointPosGhost1 = initialPosGhost1;
            ghost1.GetComponent<Rigidbody>().position = initialPosGhost1;
            ghost1.GetComponent<PlayerMovement>().enabled = false;
            ghost1.transform.position = initialPosGhost1;
            ghost1.GetComponent<PlayerMovement>().enabled = true;
            AddElements.Instance.AddGhost(ghost1);
        }

        ghost2 = GameObject.FindGameObjectWithTag("Player2");
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

    
    public void SetLastCheckPointPos(Vector3 lastCheckPointPos)
    {
        this.lastCheckPointPosGhost1 = lastCheckPointPos;

    }
    public void SetLastCheckPointPos2(Vector3 lastCheckPointPos)
    {
        //Debug.Log("Ghost 2 checkpoint setted at" + lastCheckPointPos);
        //lastCheckPointPos.y += 1.5f;
        this.lastCheckPointPosGhost2 = lastCheckPointPos;
    }

    public void SetActiveCheckpoint1(bool aux)
    {
        activeCheckpoint1 = aux;
    }
     public void SetActiveCheckpoint2(bool aux)
    {
        activeCheckpoint2 = aux;
    }

    public bool ActiveCheckpoint1()
    {
        return activeCheckpoint1;
    }
    public bool ActiveCheckpoint2()
    {
        return activeCheckpoint2;
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
