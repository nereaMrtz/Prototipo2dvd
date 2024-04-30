using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereCinematicEnd : MonoBehaviour
{
    [SerializeField] private GameObject brokenSphere;
    [SerializeField] private float timeToStopCamera = 5.0f;
    [SerializeField] Fog fog;

    [SerializeField] AudioClip breakClip;
    [SerializeField] string breakClipName;

    private void OnTriggerEnter(Collider other)
    {
        //AddElements.Instance.AddElement(other.gameObject);
        //Destroy(other.gameObject);
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        brokenSphere.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SphereBreak");
        StartCoroutine(WaitThenDestroy(other.gameObject));
    }


    IEnumerator WaitThenDestroy(GameObject Sphere)
    {

        Debug.Log("a" + Sphere.transform);
        yield return new WaitForSeconds(timeToStopCamera);
        Sphere.SetActive(false);
        //Destroy(Sphere);
        Debug.Log("finalisasion");
        GetComponent<DialogTrigger>().StartDialog();
    }

    private void Update()
    {
        if (GetComponent<DialogTrigger>().GetDialogDone())
        {
            fog.gameObject.SetActive(true);
            fog.active = true;
        }
    }
}
