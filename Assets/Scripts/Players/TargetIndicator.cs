using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField]private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {        
        transform.DODynamicLookAt(target.position, rotationSpeed);
    }
}
