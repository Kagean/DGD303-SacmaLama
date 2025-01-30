using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource musicSource; // M�zik kayna��
        [SerializeField] AudioSource sfxSource; // SFX kayna��

        public AudioClip MainMenuMusic;
        public AudioClip background; // Arkaplan m�zi�i
        public AudioClip bossMusic; // Boss m�zi�i
        public AudioClip bossFirstSound;

        private static AudioManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Nesnenin sahne ge�i�lerinde yok edilmemesini sa�la
            }
            else
            {
                Destroy(gameObject); // Zaten bir instance varsa, bu nesneyi yok et
                return;
            }
        }

        private void Start()
        {
            // Sahne ad�n� kontrol et ve uygun m�zi�i �al
            CheckAndPlayMusic();

            // Sahne de�i�ikliklerini dinle
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            // Sahne de�i�ikliklerini dinlemeyi b�rak
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Sahne ad�n� kontrol et ve uygun m�zi�i �al
            CheckAndPlayMusic();
        }

        private void CheckAndPlayMusic()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log($"Y�klenen sahne: {sceneName}");

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
            Debug.Log("Ana men� m�zi�i �al�n�yor.");
            musicSource.clip = MainMenuMusic;
            musicSource.Play();
        }

        private void PlayBackgroundMusic()
        {
            Debug.Log("Arkaplan m�zi�i �al�n�yor.");
            musicSource.clip = background;
            musicSource.Play();
        }
    }
}
