using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverUI; // Game Over ekran� referans�
        public TextMeshProUGUI ScoreText; // Puan� g�stermek i�in bir UI Text
        public GameObject spawner; // Spawner GameObject referans�

        public void Setup(int score)
        {
            gameOverUI.SetActive(true); // Game Over ekran�n� g�r�n�r yap
            ScoreText.text = "Score: " + score.ToString(); // Puan� UI'da g�ster

            if (spawner != null)
            {
                spawner.SetActive(false); // Spawner'� devre d��� b�rak
            }
        }
    }
}
