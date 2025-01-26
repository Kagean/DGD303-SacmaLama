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

    uint numDestroies = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Level1", "Level2" }; // B�l�mler
    int currentlevel = 1;

    public int score = 0; // Puan de�i�keni public yap�ld�.
    TextMeshProUGUI scoreText;
    public GameOverScreen gameOverScreen; // GameOverScreen referans�

    // Hedef �ld�rme say�s�n� de�i�tirebilece�iniz public bir de�i�ken
    public int targetDestroiesToNextLevel = 50; // Bu say�y� Unity edit�r�nden de�i�tirebilirsiniz

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
                gameOverScreen.Setup(score);
            }
            return; // Oyuncu yoksa di�er i�lemleri yapmaya gerek yok
        }

        if (Input.GetKeyDown(KeyCode.R)) // �rnek: "R" tu�una bas�ld���nda s�f�rlama
        {
            ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnMainMenu();
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

        // Skoru ve can� s�f�rl�yoruz
        score = 0;
        scoreText.text = score.ToString();

        // Player can�n� 3'e s�f�rl�yoruz
        if (playerController != null)
        {
            playerController.health = 3;
        }

        // Hearths'� da tekrar yeniliyoruz
        foreach (GameObject hearth in hearths)
        {
            hearth.SetActive(true);
        }

        // T�m mermileri sil
        foreach (Bullet b in bullets)
        {
            Debug.Log("Mermi siliniyor: " + b.gameObject.name);
            Destroy(b.gameObject);
        }
        bullets.Clear(); // After destroying, clear the list

        // �ld�rme say�s�n� s�f�rl�yoruz
        numDestroies = 0;

        // Bir sonraki b�l�me ge�i�i engelliyoruz (startNextLevel'� false yap�yoruz)
        startNextLevel = false;

        // Sahneyi yeniden y�kle
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Sahne yeniden y�kleniyor: " + currentSceneName);
        SceneManager.LoadScene(currentSceneName);
    }

    public void ReturnMainMenu()
    {
        Debug.Log("Men�ye d�n�ld�");
        SceneManager.LoadScene("MainMenu");
    }

    public void AddDestroy()
    {
        numDestroies++;
        Debug.Log("Destroyed: " + numDestroies);

        // E�er hedef �ld�rme say�s� targetDestroiesToNextLevel de�erine ula��rsa, bir sonraki b�l�me ge�i�i ba�lat
        if (numDestroies >= targetDestroiesToNextLevel)
        {
            startNextLevel = true;
        }
    }

    public void RemoveDestroy()
    {
        numDestroies--;
        Debug.Log("Destroyed: " + numDestroies);

        // E�er hedef �ld�rme say�s� targetDestroiesToNextLevel de�erine ula��rsa, bir sonraki b�l�me ge�i�i ba�lat
        if (numDestroies >= targetDestroiesToNextLevel)
        {
            startNextLevel = true;
        }
    }

    // Bu fonksiyon, b�l�m� ge�mek i�in gerekli olan ko�ul sa�land���nda �a�r�l�r
    public void CheckLevelComplete()
    {
        if (numDestroies >= targetDestroiesToNextLevel) // E�er hedef �ld�rme say�s� hedefe ula��rsa
        {
            startNextLevel = true; // Bir sonraki b�l�me ge�i� ba�lat
        }
    }

    // Method to add bullets to the list when they are instantiated
    public void AddBullet(Bullet newBullet)
    {
        bullets.Add(newBullet);
    }
}
