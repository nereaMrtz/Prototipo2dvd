using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [HideInInspector]
    public string id;

    private void Awake()
    {
        id = name + transform.position.ToString();
    }

    private void Start()
    {


        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i].id == id)
                {
                    Destroy(gameObject);
                }
            }
        }
        this.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

}
