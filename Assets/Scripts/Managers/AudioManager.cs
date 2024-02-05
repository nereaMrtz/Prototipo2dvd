using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioSource SFXSource;
    Dictionary<string, AudioClip> SFX;

    [SerializeField] AudioMixer mixer; 
    public static AudioManager Instance { get; private set; }

    bool masterMute=false;
    bool bgMute=false;
    bool sfxMute=false;
    
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

    public bool LoadSFX(string clipName, AudioClip clip)
    {
        if (clip.LoadAudioData())
        {
            SFX.Add(clipName, clip);
            return true;
        }
        else return false;
    }

    public void PlaySFX(string clipName)
    {
        SFXSource.clip = SFX[clipName];
        SFXSource.Play();

    }

    public void MuteMaster()
    {
        if (!masterMute)
        {
            mixer.SetFloat("Master", -80f);
            masterMute = true;
        }
        else
        {
            mixer.SetFloat("Master", 0f);
            masterMute = false;
        }
    }

    public void MuteBackground()
    {
        if (!bgMute)
        {
            mixer.SetFloat("BackgroundMusic", -80f);
            bgMute = true;
        }
        else
        {
            mixer.SetFloat("BackgroundMusic", 0f);
            bgMute = false;
        }
    }

    public void MuteSFX()
    {
        if (!sfxMute)
        {
            mixer.SetFloat("SoundEffects", -80f);
            sfxMute = true;
        }
        else
        {
            mixer.SetFloat("SoundEffects", 0f);
            sfxMute = false;
        }
    }
}