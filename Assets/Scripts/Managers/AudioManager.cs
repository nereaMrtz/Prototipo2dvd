using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource button;
    [SerializeField] public AudioSource maldicion;
    [SerializeField] public AudioSource music;
    [SerializeField] public AudioSource wind;
    [SerializeField] public AudioSource door_platform;
    [SerializeField] public AudioSource jump;

    public static AudioManager Instance { get; private set; }

    [HideInInspector] public bool load = false;
    [SerializeField] private Animator transition;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one AudioManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}