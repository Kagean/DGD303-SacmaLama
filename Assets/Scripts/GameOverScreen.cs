using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject GameOverPanel; // Game Over ekran� referans�

        // Game Over ekran�n� a�an metod
        public void Setup()
        {
            GameOverPanel.SetActive(true); // Game Over ekran�n� g�r�n�r yap
            Time.timeScale = 0f; // Oyunu durdur
        }

        // Game Over ekran�n� kapatan metod
        public void UnSetup()
        {
            GameOverPanel.SetActive(false); // Game Over ekran�n� g�r�nmez yap
            Time.timeScale = 1f; // Oyunu devam ettir
        }
    }
}
