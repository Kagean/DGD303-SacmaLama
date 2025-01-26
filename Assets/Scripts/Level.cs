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
    string[] levels = { "Level1", "Level2" }; // Bölümler
    int currentlevel = 1;

    public int score = 0; // Puan deðiþkeni public yapýldý.
    TextMeshProUGUI scoreText;
    public GameOverScreen gameOverScreen; // GameOverScreen referansý

    private List<Bullet> bullets = new List<Bullet>(); // List to track bullets

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Sahne geçiþlerinde yok olmamasý için
            DontDestroyOnLoad(gameObject);

            // Referanslarý doðru þekilde baðlayýn
            GameObject scoreTextObject = GameObject.Find("ScoreText");
            if (scoreTextObject != null)
            {
                scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("ScoreText GameObject bulunamadý!");
            }

            // Player referansýný baðla
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerController = playerObject.GetComponent<PlayerController>();
            }
            else
            {
                Debug.LogError("Player GameObject bulunamadý!");
            }
        }
        else
        {
            Destroy(gameObject); // Eðer daha önce bir instance varsa, yeni instance'ý yok et
        }
    }

    void Update()
    {
        // Oyuncu yoksa Game Over ekranýný göster
        if (playerController == null)
        {
            if (gameOverScreen != null && !gameOverScreen.gameOverUI.activeSelf)
            {
                gameOverScreen.Setup();
            }
            return; // Oyuncu yoksa diðer iþlemleri yapmaya gerek yok
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

        float health = playerController.health; // Saðlýk deðerini al
        Debug.Log($"Player Health: {health}");

        // Saðlýk durumuna göre UI güncelle
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
        Debug.Log("ResetLevel çaðrýldý.");

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

        // Sahneyi yeniden yükle
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void AddBullet(Bullet newBullet)
    {
        bullets.Add(newBullet);
    }
}
