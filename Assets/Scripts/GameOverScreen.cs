using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverUI; // Game Over ekraný referansý
        public void Setup(int score)
        {
            gameOverUI.SetActive(true); // Game Over ekranýný görünür yap
        }
    }
}
