using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatformManager : MonoBehaviour
{
    [SerializeField] BotonPlatHorizontal boton1;
    [SerializeField] BotonPlatHorizontal boton2;
    [SerializeField] BotonPlatHorizontal boton3;

    void Update()
    {
        if (boton1.pressed || boton2.pressed || boton3.pressed)
        {
            boton1.Move();

        }
        else
            boton1.Back();
    }
}