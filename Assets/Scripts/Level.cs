using System.Collections.Generic;
using Shmup;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance;

    public GameObject[] hearths;
    public PlayerController playerController;

    bool startNextLevel = false;
    float nextLevelTimer = 3;
    string[] levels = { "Level1", "Level2" }; // B�l�mler
    int currentlevel = 1;

    public int score = 0; // Puan de�i�keni public yap�ld�.
    TextMeshProUGUI scoreText;
    public GameOverScreen gameOverScreen; // GameOverScreen referans�

    private List<Bullet> bullets = new List<Bullet>(); // List to track bullets

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Sahne ge�i�lerinde yok olmamas� i�in
            DontDestroyOnLoad(gameObject);

            // Referanslar� do�ru �ekilde ba�lay�n
            GameObject scoreTextObject = GameObject.Find("ScoreText");
            if (scoreTextObject != null)
            {
                scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("ScoreText GameObject bulunamad�!");
            }

            // Player referans�n� ba�la
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerController = playerObject.GetComponent<PlayerController>();
            }
            else
            {
                Debug.LogError("Player GameObject bulunamad�!");
            }
        }
        else
        {
            Destroy(gameObject); // E�er daha �nce bir instance varsa, yeni instance'� yok et
        }
    }

    void Update()
    {
        // Oyuncu yoksa Game Over ekran�n� g�ster
        if (playerController == null)
        {
            if (gameOverScreen != null && !gameOverScreen.gameOverUI.activeSelf)
            {
                gameOverScreen.Setup();
            }
            return; // Oyuncu yoksa di�er i�lemleri yapmaya gerek yok
        }

        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentlevel++;
                if (currentlevel <= levels.Length)
                {
                    string scenename = levels[currentlevel - 1];
                    SceneManager.LoadSceneAsync(scenename);
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }

        float health = playerController.health; // Sa�l�k de�erini al
        Debug.Log($"Player Health: {health}");

        // Sa�l�k durumuna g�re UI g�ncelle
        if (playerController.health < 1)
        {
            Destroy(hearths[0].gameObject);
        }
        else if (playerController.health < 2)
        {
            Destroy(hearths[1].gameObject);
        }
        else if (playerController.health < 3)
        {
            Destroy(hearths[2].gameObject);
        }
    }


    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();
    }

    public void ResetLevel()
    {
        Debug.Log("ResetLevel �a�r�ld�.");

        score = 0;
        scoreText.text = score.ToString();

        if (playerController != null)
        {
            playerController.health = 3;
        }

        foreach (GameObject hearth in hearths)
        {
            hearth.SetActive(true);
        }

        foreach (Bullet b in bullets)
        {
            Destroy(b.gameObject);
        }
        bullets.Clear();
        gameOverScreen.ReSetup();

        // Sahneyi yeniden y�kle
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void AddBullet(Bullet newBullet)
    {
        bullets.Add(newBullet);
    }
}
