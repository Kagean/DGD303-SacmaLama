using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject GameOverPanel;


        public void Setup()
        {
            if (GameOverPanel != null)
            {
                GameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        public void UnSetup()
        {
            GameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
