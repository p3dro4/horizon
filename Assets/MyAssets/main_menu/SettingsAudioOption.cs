using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.main_menu
{
    public class SettingsAudioOption : MonoBehaviour
    {
        [SerializeField] private AudioSourceTypes audioType;
        [SerializeField] private Settings settings;
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = 1;
            _slider.minValue = 0;
            _slider.value = audioType switch
            {
                AudioSourceTypes.Master => settings.MasterVolume,
                AudioSourceTypes.Sfx => settings.SfxVolume,
                AudioSourceTypes.Music => settings.MusicVolume,
                _ => _slider.value
            };
        }

        private void Update()
        {
            switch (audioType)
            {
                case AudioSourceTypes.Master:
                    settings.MasterVolume = _slider.value;
                    break;
                case AudioSourceTypes.Sfx:
                    settings.SfxVolume = _slider.value;
                    break;
                case AudioSourceTypes.Music:
                    settings.MusicVolume = _slider.value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}