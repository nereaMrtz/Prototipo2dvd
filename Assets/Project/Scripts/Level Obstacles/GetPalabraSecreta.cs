using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPalabraSecreta : MonoBehaviour
{
    [SerializeField] PalabraSecretaInteraction Interaction;

    private void OnTriggerEnter(Collider other)
    {
        Interaction.interactActive = true;
    }
}
