using UnityEngine;
using UnityEngine.Serialization;

namespace MyAssets
{
    [CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings", order = 16)]
    public class Settings : ScriptableObject
    {
        [Header("Default Values")] [Space(2)] [Range(0, 1)] [SerializeField]
        private float defaultMasterVolume = 1f;

        [Range(0, 1)] [SerializeField] private float defaultSfxVolume = 1f;
        [Range(0, 1)] [SerializeField] private float defaultMusicVolume = 1f;

        [Space(10)] [Header("Current Values")] [Space(2)] [Range(0, 1)] [SerializeField]
        private float masterVolume = 1f;

        [Range(0, 1)] [SerializeField] private float sfxVolume = 1f;
        [Range(0, 1)] [SerializeField] private float musicVolume = 1f;

        private void OnEnable()
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume);
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume", defaultSfxVolume);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume);
        }


        public float MasterVolume
        {
            get => masterVolume;
            set => masterVolume = value;
        }

        public float SfxVolume
        {
            get => sfxVolume;
            set => sfxVolume = value;
        }

        public float MusicVolume
        {
            get => musicVolume;
            set => musicVolume = value;
        }

        public void ResetDefaults()
        {
            masterVolume = defaultMasterVolume;
            sfxVolume = defaultSfxVolume;
            musicVolume = defaultMusicVolume;
        }
    }
}