using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCinematicEnd : MonoBehaviour
{
    [SerializeField] private GameObject brokenSphere;
    [SerializeField] private float timeToStopCamera = 5.0f;
    private void OnTriggerEnter(Collider other)
    {
        //AddElements.Instance.AddElement(other.gameObject);
        //Destroy(other.gameObject);
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        brokenSphere.SetActive(true);
        StartCoroutine(WaitThenDestroy(other.gameObject));
    }


    IEnumerator WaitThenDestroy(GameObject Sphere)
    {

        Debug.Log("a" + Sphere.transform);
        yield return new WaitForSeconds(timeToStopCamera);
        Sphere.SetActive(false);
        //Destroy(Sphere);
    }
}
