using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueGetter : MonoBehaviour
{
    public Slider MasterVolume;
    [SerializeField] Slider BgVolume;
    [SerializeField] Slider SFXVolume;

    AudioManager AM;
    private void Awake()
    {
        AM = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
    }
    public void SetMaster()
    {
        AM.SetMasterVolume(MasterVolume.value);
    }
}
