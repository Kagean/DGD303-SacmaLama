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
            GameOverPanel.SetActive(true); // Game Over ekranýný görünür yap
            Time.timeScale = 0f; // Oyunu durdur
        }

        // Game Over ekranýný kapatan metod
        public void UnSetup()
        {
            GameOverPanel.SetActive(false); // Game Over ekranýný görünmez yap
            Time.timeScale = 1f; // Oyunu devam ettir
        }
    }
}
