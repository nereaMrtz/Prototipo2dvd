using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distancebetweenplayers : MonoBehaviour
{
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(target1.transform.position, target2.transform.position));
    }
}
