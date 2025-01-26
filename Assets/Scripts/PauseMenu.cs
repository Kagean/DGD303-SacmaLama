using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenuUI;

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Home()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Restart()
        {
            Level.Instance.ResetLevel();
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

    }
}
