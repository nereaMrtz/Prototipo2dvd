using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioSource SFXSource;
    Dictionary<string, AudioClip> SFX;
    public static AudioManager Instance;
    [SerializeField] AudioMixer mixer;
    public bool masterMute { get; private set; } = false;
    float prevMasterVolume;
    public bool bgMute { get; private set; } = false;
    float prevBgVolume;
    public bool sfxMute { get; private set; } = false;
    float prevSfxVolume;

    [HideInInspector] public bool load = false;
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

        backgroundSource.clip = backgroundMusic[0];
        backgroundSource.Play(); ;
    }

    public static AudioManager GetInstance()
    {
        return Instance;
    }

    public bool LoadSFX(string clipName, AudioClip clip)
    {
        if (HasSFX(clipName))
        {
            return true;
        }
        else if (clip.LoadAudioData())
        {
            SFX.Add(clipName, clip);
            return true;
        }
        else return false;
    }

    bool HasSFX(string clipName)
    {
        bool a;
        a= SFX.ContainsKey(clipName);
        Debug.Log(a);
        return a;
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
            mixer.GetFloat("Master", out prevMasterVolume);
            mixer.SetFloat("Master", -80f);
            masterMute = true;
        }
        else
        {
            mixer.SetFloat("Master", prevMasterVolume);
            masterMute = false;
        }
    }

    public void MuteBackground()
    {
        if (!bgMute)
        {
            mixer.GetFloat("BackgroundMusic", out prevBgVolume);
            mixer.SetFloat("BackgroundMusic", -80f);
            bgMute = true;
        }
        else
        {
            mixer.SetFloat("BackgroundMusic", prevBgVolume);
            bgMute = false;
        }
    }

    public void MuteSFX()
    {
        if (!sfxMute)
        {
            mixer.GetFloat("SoundEffects", out prevSfxVolume);
            mixer.SetFloat("SoundEffects", -80f);
            sfxMute = true;
        }
        else
        {
            mixer.SetFloat("SoundEffects", prevSfxVolume);
            sfxMute = false;
        }
    }

    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBgVolume(float volume)
    {
        mixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SoundEffects", Mathf.Log10(volume) * 20);
    }
}
