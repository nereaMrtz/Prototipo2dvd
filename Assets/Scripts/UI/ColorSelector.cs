using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    [SerializeField] GameObject ghost1;
    [SerializeField] GameObject ghost2;

    public void ChangeColor(Material button)
    {
        ghost1.GetComponent<SkinnedMeshRenderer>().material = button;
        ghost2.GetComponent<SkinnedMeshRenderer>().material = button;
    }

    public Material ApplyColor()
    {
        //Esto se cambiara a un void que guarde el material en el script de cada player
        return ghost1.GetComponent<SkinnedMeshRenderer>().material;
    }
}
