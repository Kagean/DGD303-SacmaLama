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
        [SerializeField] Button SettingsButton;
        [SerializeField] Button quitButton;

        void Awake()
        {
            playButton.onClick.AddListener(() => Loader.Loading(startingLevel));
            SettingsButton.onClick.AddListener(() => Helpers.QuitGame());
            quitButton.onClick.AddListener(() => Helpers.QuitGame());
            Time.timeScale = 1f;
        }
    }
}