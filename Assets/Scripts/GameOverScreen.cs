using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverUI; // Game Over ekran� referans�
        public void Setup(int score)
        {
            gameOverUI.SetActive(true); // Game Over ekran�n� g�r�n�r yap
        }
    }
}
