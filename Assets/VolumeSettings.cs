using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Shmup
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] AudioMixer myMixer;
        [SerializeField] Slider musicSlider;
        [SerializeField] Slider sfxSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                LoadVolumeSettings();
            }
            else
            {
                SetMusicVolume();
                SetSfxVolume();
            }
        }

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
        }

        public void SetSfxVolume()
        {
            float volume = sfxSlider.value;
            myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("sfxVolume", volume);
        }

        public void LoadVolumeSettings()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

            SetMusicVolume();
            SetSfxVolume();
        }
    }
}
