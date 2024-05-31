using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    public Vector3 direction;
    public float lifeTime;

    private void Update()
    {
        if (!(lifeTime < 0))
        {
            transform.position += direction * Time.deltaTime;
            lifeTime -= Time.deltaTime;
        }
        else
            StartCoroutine(DestroyThis());
        
    }
        IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(1);
            Destroy(this.gameObject);
        }
}
