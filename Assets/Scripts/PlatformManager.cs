using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] PalancaPlataforma palanca1;
    [SerializeField] PalancaPlataforma palanca2;
    [SerializeField] PalancaPlataforma palanca3;

    void Update()
    {
        if (palanca1.pressed || palanca2.pressed || palanca3.pressed)
        {
            palanca1.Move();

        }
        else
            palanca1.Back();
    }
}