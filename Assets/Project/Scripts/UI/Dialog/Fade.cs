using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade Instance;

    [SerializeField]private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one Fade!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeOut()
    {
        animator.Play("FadeOut");
    }

    public void FadeIn()
    {
        animator.Play("FadeAnimation");
        Debug.Log("fade");
    }
}
