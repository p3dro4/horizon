using UnityEngine;

namespace MyAssets.main_menu
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSourceTypes audioType;
        [SerializeField] private Settings settings;


        private void Update()
        {
            audioSource.volume =
                settings.MasterVolume * (audioType == AudioSourceTypes.Sfx ? settings.SfxVolume : settings.MusicVolume);
        }
    }
}