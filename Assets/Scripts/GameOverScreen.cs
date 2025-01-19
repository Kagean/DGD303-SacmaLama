using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverUI; // Game Over ekraný referansý
        public TextMeshProUGUI ScoreText; // Puaný göstermek için bir UI Text
        public GameObject spawner; // Spawner GameObject referansý

        public void Setup(int score)
        {
            gameOverUI.SetActive(true); // Game Over ekranýný görünür yap
            ScoreText.text = "Score: " + score.ToString(); // Puaný UI'da göster

            if (spawner != null)
            {
                spawner.SetActive(false); // Spawner'ý devre dýþý býrak
            }
        }
    }
}
