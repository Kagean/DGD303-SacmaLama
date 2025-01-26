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

    string[] levels = { "Level1", "Level2" }; // Bölümler
    int currentlevel = 1;

    public int score = 0; // Puan deðiþkeni public yapýldý.
    TextMeshProUGUI scoreText;
    public GameOverScreen gameOverScreen; // GameOverScreen referansý

    // Hedef öldürme sayýsýný deðiþtirebileceðiniz public bir deðiþken
    public int targetDestroiesToNextLevel = 50; // Bu sayýyý Unity editöründen deðiþtirebilirsiniz

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
                gameOverScreen.Setup(score);
            }
            return; // Oyuncu yoksa diðer iþlemleri yapmaya gerek yok
        }

        if (Input.GetKeyDown(KeyCode.R)) // Örnek: "R" tuþuna basýldýðýnda sýfýrlama
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

        // Skoru ve caný sýfýrlýyoruz
        score = 0;
        scoreText.text = score.ToString();

        // Player canýný 3'e sýfýrlýyoruz
        if (playerController != null)
        {
            playerController.health = 3;
        }

        // Hearths'ý da tekrar yeniliyoruz
        foreach (GameObject hearth in hearths)
        {
            hearth.SetActive(true);
        }

        // Tüm mermileri sil
        foreach (Bullet b in bullets)
        {
            Debug.Log("Mermi siliniyor: " + b.gameObject.name);
            Destroy(b.gameObject);
        }
        bullets.Clear(); // After destroying, clear the list

        // Öldürme sayýsýný sýfýrlýyoruz
        numDestroies = 0;

        // Bir sonraki bölüme geçiþi engelliyoruz (startNextLevel'ý false yapýyoruz)
        startNextLevel = false;

        // Sahneyi yeniden yükle
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Sahne yeniden yükleniyor: " + currentSceneName);
        SceneManager.LoadScene(currentSceneName);
    }

    public void ReturnMainMenu()
    {
        Debug.Log("Menüye dönüldü");
        SceneManager.LoadScene("MainMenu");
    }

    public void AddDestroy()
    {
        numDestroies++;
        Debug.Log("Destroyed: " + numDestroies);

        // Eðer hedef öldürme sayýsý targetDestroiesToNextLevel deðerine ulaþýrsa, bir sonraki bölüme geçiþi baþlat
        if (numDestroies >= targetDestroiesToNextLevel)
        {
            startNextLevel = true;
        }
    }

    public void RemoveDestroy()
    {
        numDestroies--;
        Debug.Log("Destroyed: " + numDestroies);

        // Eðer hedef öldürme sayýsý targetDestroiesToNextLevel deðerine ulaþýrsa, bir sonraki bölüme geçiþi baþlat
        if (numDestroies >= targetDestroiesToNextLevel)
        {
            startNextLevel = true;
        }
    }

    // Bu fonksiyon, bölümü geçmek için gerekli olan koþul saðlandýðýnda çaðrýlýr
    public void CheckLevelComplete()
    {
        if (numDestroies >= targetDestroiesToNextLevel) // Eðer hedef öldürme sayýsý hedefe ulaþýrsa
        {
            startNextLevel = true; // Bir sonraki bölüme geçiþ baþlat
        }
    }

    // Method to add bullets to the list when they are instantiated
    public void AddBullet(Bullet newBullet)
    {
        bullets.Add(newBullet);
    }
}
