using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRespawn : MonoBehaviour
{
    private Vector3 initPos;

    private Rigidbody rb;

    [SerializeField] private string clipName;
    [SerializeField] private AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;    
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -50f)
        {
            Respawn();
        }
        transform.DOScale(new Vector3(1.75f, 1.75f, 1.75f), 0.01f);
    }

    public void Respawn()
    {
        transform.position = initPos;
        rb.velocity = Vector3.zero;
        rb.AddForce(rb.velocity);
        //if(AudioManager.Instance.LoadSFX(clipName, clip))
        //{
        //    AudioManager.Instance.PlaySFX(clipName);
        //}
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Electricity")
        {
            Respawn();
        }
    }
}
