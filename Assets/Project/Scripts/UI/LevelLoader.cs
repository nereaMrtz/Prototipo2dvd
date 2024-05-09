using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader Instance { get; private set; }

    [HideInInspector] public bool load = false;
    [SerializeField]private Animator transition;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelLoader!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }   
  
    private void Update()
    {
        if (load)
            transition.SetTrigger("Start");
    }

    public void SetLoad()
    {
        load = true;
    }
}
