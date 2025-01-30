using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource musicSource; // Müzik kaynaðý
        [SerializeField] AudioSource sfxSource; // SFX kaynaðý

        public AudioClip MainMenuMusic;
        public AudioClip background; // Arkaplan müziði
        public AudioClip bossMusic; // Boss müziði
        public AudioClip bossFirstSound;

        private static AudioManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Nesnenin sahne geçiþlerinde yok edilmemesini saðla
            }
            else
            {
                Destroy(gameObject); // Zaten bir instance varsa, bu nesneyi yok et
                return;
            }
        }

        private void Start()
        {
            // Sahne adýný kontrol et ve uygun müziði çal
            CheckAndPlayMusic();

            // Sahne deðiþikliklerini dinle
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CheckAndPlayMusic();
        }

        private void CheckAndPlayMusic()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "Level1" || sceneName == "Level2")
            {
                PlayBackgroundMusic();
            }
            else
            {
                PlayMainMenuMusic();
            }
        }

        private void PlayMainMenuMusic()
        {
            musicSource.clip = MainMenuMusic;
            musicSource.Play();
        }

        private void PlayBackgroundMusic()
        {
            musicSource.clip = background;
            musicSource.Play();
        }
    }
}
