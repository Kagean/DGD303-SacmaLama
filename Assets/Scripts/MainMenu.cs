using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] SceneReference startingLevel;
        [SerializeField] Button playButton;
        [SerializeField] Button quitButton;
        [SerializeField] Button CreditsButton;

        void Awake()
        {
            playButton.onClick.AddListener(() => Loader.Loading(startingLevel));
            Time.timeScale = 1f;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void Credits()
        {
            SceneManager.LoadScene("Level3");
        }

    }
}