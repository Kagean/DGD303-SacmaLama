using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Shmup
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] SceneReference startingLevel;
        [SerializeField] Button playButton;
        [SerializeField] Button quitButton;

        void Awake()
        {
            playButton.onClick.AddListener(() => Loader.Loading(startingLevel));
            Time.timeScale = 1f;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}