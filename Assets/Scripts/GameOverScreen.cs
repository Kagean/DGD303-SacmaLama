using TMPro;
using UnityEngine;

namespace Shmup
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverUI; // Game Over ekran� referans�
        public void Setup()
        {
            gameOverUI.SetActive(true); // Game Over ekran�n� g�r�n�r yap
        }

        public void ReSetup()
        {
            gameOverUI.SetActive(false); // Game Over ekran�n� g�r�n�r yap
        }
    }
}
