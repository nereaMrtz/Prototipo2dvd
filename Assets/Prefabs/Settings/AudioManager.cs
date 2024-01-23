using System;
using Project.Scripts.NoMonoBehaviourClass;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        
        private const String PLAYERS_PREFS_MASTER_VOLUME_VALUE = "Player Prefs Master Volume Value";
        private const String PLAYERS_PREFS_SFX_VOLUME_VALUE = "Player Prefs SFX Volume Value";
        private const String PLAYERS_PREFS_MUSIC_VOLUME_VALUE = "Player Prefs Music Volume Value";
        
        private const String PLAYERS_PREFS_MASTER_MUTE = "Player Prefs Master Mute";
        private const String PLAYERS_PREFS_SFX_MUTE = "Player Prefs SFX Mute";
        private const String PLAYERS_PREFS_MUSIC_MUTE = "Player Prefs Music Mute";

        private const String AMBIENT = "Ambiente";
        private const string BOTON_MENU = "BotonMenu";

        [SerializeField] private AudioMixer _audioMixer;
        
        [SerializeField] private String _masterVolumeMixer;
        [SerializeField] private String _SFXVolumeMixer;
        [SerializeField] private String _musicVolumeMixer;
        
        [SerializeField] private Sound[] _sounds;

        private AudioSource _audioSourceAmbient;
        private AudioSource _audioSourceMenuButton;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                
                if (!PlayerPrefs.HasKey(PLAYERS_PREFS_MASTER_VOLUME_VALUE))
                {
                    
                    PlayerPrefs.SetFloat(PLAYERS_PREFS_MASTER_VOLUME_VALUE, -15);
                    PlayerPrefs.SetFloat(PLAYERS_PREFS_SFX_VOLUME_VALUE, -15);
                    PlayerPrefs.SetFloat(PLAYERS_PREFS_MUSIC_VOLUME_VALUE, -15);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

            _audioSourceAmbient = gameObject.AddComponent<AudioSource>();
            _audioSourceMenuButton = gameObject.AddComponent<AudioSource>();
            SetAudioSourceComponent(_audioSourceAmbient, AMBIENT);
            SetAudioSourceComponent(_audioSourceMenuButton, BOTON_MENU);
        }

        private void Start()
        {
            if (PlayerPrefs.GetInt(PLAYERS_PREFS_MASTER_MUTE) == 1)
            {
                SetVolumeValue(PLAYERS_PREFS_MASTER_VOLUME_VALUE, -80);
            }
            else
            {
                SetVolumeValue(PLAYERS_PREFS_MASTER_VOLUME_VALUE, PlayerPrefs.GetFloat(PLAYERS_PREFS_MASTER_VOLUME_VALUE));
            }

            if (PlayerPrefs.GetInt(PLAYERS_PREFS_SFX_MUTE) == 1)
            {
                SetVolumeValue(PLAYERS_PREFS_SFX_VOLUME_VALUE, -80);
            }
            else
            {
                SetVolumeValue(PLAYERS_PREFS_SFX_VOLUME_VALUE, PlayerPrefs.GetFloat(PLAYERS_PREFS_SFX_VOLUME_VALUE));
            }

            if (PlayerPrefs.GetInt(PLAYERS_PREFS_MUSIC_MUTE) == 1)
            {
                SetVolumeValue(PLAYERS_PREFS_MUSIC_VOLUME_VALUE, -80);
            }
            else
            {
                SetVolumeValue(PLAYERS_PREFS_MUSIC_VOLUME_VALUE, PlayerPrefs.GetFloat(PLAYERS_PREFS_MUSIC_VOLUME_VALUE));
            }
            
        }

        public static AudioManager Instance
        {
            get { return _instance; }
        }
        
        public void SetAudioSourceComponent(AudioSource audioSource, string name)
        {
            foreach (Sound sound in _sounds)
            {
                if (sound.GetName() != name)
                {
                    continue;
                }
                sound.SetAudioSource(audioSource);
                sound.GetAudioSource().outputAudioMixerGroup = sound.GetAudioMixerGroup();
                sound.GetAudioSource().clip = sound.GetClip();
                sound.GetAudioSource().volume = sound.GetVolume();
                sound.GetAudioSource().loop = sound.GetLoop();
                sound.GetAudioSource().spatialBlend = Convert.ToSingle(sound.GetSound3D());
                if (sound.GetSound3D())
                {
                    sound.GetAudioSource().rolloffMode = AudioRolloffMode.Linear;
                    sound.GetAudioSource().minDistance = 0;
                    sound.GetAudioSource().maxDistance = 30;    
                }

                sound.GetAudioSource().playOnAwake = false;

                if (sound.GetPlay())
                {
                    sound.GetAudioSource().Play();
                }

                return;
            }
        }

        public void PlayMenuButtonSound()
        {
            _audioSourceMenuButton.Play();
        }

        public void SetVolumePrefs(String playerPrefsVolumeName, float volumeValue)
        {
            PlayerPrefs.SetFloat(playerPrefsVolumeName, volumeValue);
            SetVolumeValue(playerPrefsVolumeName, volumeValue);
        }

        public void SetVolumeValue(String playerPrefsVolumeName, float volumeValue)
        {
            switch (playerPrefsVolumeName)
            {
                case PLAYERS_PREFS_MASTER_VOLUME_VALUE:
                    _audioMixer.SetFloat(_masterVolumeMixer, volumeValue);
                    break;
                case PLAYERS_PREFS_SFX_VOLUME_VALUE:
                    _audioMixer.SetFloat(_SFXVolumeMixer, volumeValue);
                    break;
                default:
                    _audioMixer.SetFloat(_musicVolumeMixer, volumeValue);
                    break;
            }
        }
    }
}