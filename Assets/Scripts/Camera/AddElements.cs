using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AddElements : MonoBehaviour
{
    public static AddElements Instance { get; private set; }

    private CinemachineTargetGroup cinemachineTargetGroup;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one AddElements!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }


    public void AddElement(GameObject gameObject)
    {
        cinemachineTargetGroup.AddMember(gameObject.transform, 1, 4);
    }

    public void RemoveElement(GameObject gameObject) 
    { 
        cinemachineTargetGroup.RemoveMember(gameObject.transform);
    }
}
