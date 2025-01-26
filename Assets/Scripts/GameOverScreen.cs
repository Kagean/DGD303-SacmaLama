using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverUI; // Game Over ekraný referansý
        public void Setup()
        {
            gameOverUI.SetActive(true); // Game Over ekranýný görünür yap
        }

        public void ReSetup()
        {
            gameOverUI.SetActive(false); // Game Over ekranýný görünür yap
        }
    }
}
