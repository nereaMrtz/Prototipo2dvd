using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Managers
{
    public class ResolutionManager : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _resolutionDropdown;

        private Resolution[] _resolutions;

        private List<Resolution> _availableResolutions;

        private Resolution _currentResolution;

        private float _currentRefreshRate;

        private int _resolutionIndexLength;

        private void Start()
        {
            _resolutions = Screen.resolutions;
            _availableResolutions = new List<Resolution>();
            _resolutionDropdown.ClearOptions();
            _currentRefreshRate = Screen.currentResolution.refreshRate;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                if (_resolutions[i].refreshRate == _currentRefreshRate)
                {
                    _availableResolutions.Add(_resolutions[i]);
                }
            }

            List<String> options = new List<string>();

            for (int i = 0; i < _availableResolutions.Count; i++)
            {
                string resolutionOption = _availableResolutions[i].width + "x" + _availableResolutions[i].height + " " + _availableResolutions[i].refreshRate + "Hz";
                options.Add(resolutionOption);
                if (_availableResolutions[i].width == Screen.width && _availableResolutions[i].height == Screen.height)
                {
                    _resolutionIndexLength = i;
                }
            }
            
            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.value = _resolutionIndexLength;
            _resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            _currentResolution = _availableResolutions[resolutionIndex];
            GameManager.Instance.SetCurrentResolution(_currentResolution);
            GameManager.Instance.ApplyResolution();
        }
    }
}