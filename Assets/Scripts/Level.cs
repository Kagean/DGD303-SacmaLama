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

    string[] levels = { "Level1", "Level2" };
    int currentlevel = 1;

    public int score = 0; // Puan de�i�keni public yap�ld�.
    TextMeshProUGUI scoreText;
    public GameOverScreen gameOverScreen; // GameOverScreen referans�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

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
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // �rnek: "R" tu�una bas�ld���nda s�f�rlama
        {
            ResetLevel();
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
                else
                {
                    Debug.Log("Game Over!!!");

                    // Game Over ekran�n� g�ster
                    scoreText.gameObject.SetActive(false); // Puan metnini gizle
                    gameOverScreen.Setup(score);  // Puan� GameOver ekran�na g�nderiyoruz
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }

        if (playerController != null)
        {
            float health = playerController.health; // Sa�l�k de�erini al
            Debug.Log($"Player Health: {health}");
        }

        // Sa�l�k durumuna g�re UI g�ncelle
        if (playerController != null && playerController.health < 1)
        {
            Destroy(hearths[0].gameObject);
        }
        else if (playerController != null && playerController.health < 2)
        {
            Destroy(hearths[1].gameObject);
        }
        else if (playerController != null && playerController.health < 3)
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

        foreach (Bullet b in FindObjectsOfType<Bullet>())
        {
            Debug.Log("Mermi siliniyor: " + b.gameObject.name);
            Destroy(b.gameObject);
        }

        numDestroies = 0;
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Sahne yeniden y�kleniyor: " + currentSceneName);
        SceneManager.LoadScene(currentSceneName);
    }

    public void AddDestroy()
    {
        numDestroies++;
    }

    public void RemoveDestroy()
    {
        numDestroies--;

        if (numDestroies == 0)
        {
            startNextLevel = true;
        }
    }
}