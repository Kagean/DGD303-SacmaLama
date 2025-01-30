using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject GameOverPanel; // Game Over ekraný referansý

        // Game Over ekranýný açan metod
        public void Setup()
        {
            if (GameOverPanel != null)
            {
                GameOverPanel.SetActive(true);
                Time.timeScale = 0f; // Oyunu duraklat
            }
        }

        // Game Over ekranýný kapatan metod
        public void UnSetup()
        {
            GameOverPanel.SetActive(false); // Game Over ekranýný görünmez yap
            Time.timeScale = 1f; // Oyunu devam ettir
        }
    }
}
