using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
   // [SerializeField] public Button[] buttons;

    [SerializeField] GameObject ghost1;
    [SerializeField] GameObject ghost2;

    public void ChangeColor(Material button)
    {
        ghost1.GetComponent<SkinnedMeshRenderer>().material = button;
        ghost2.GetComponent<SkinnedMeshRenderer>().material = button;
    }
}
