using UnityEngine;

namespace MyAssets.main_menu
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private Settings settings;

        public void ResetDefaults()
        {
            settings.ResetDefaults();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("MasterVolume", settings.MasterVolume);
            PlayerPrefs.SetFloat("SfxVolume", settings.SfxVolume);
            PlayerPrefs.SetFloat("MusicVolume", settings.MusicVolume);
        }
    }
}