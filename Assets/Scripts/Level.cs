using System.Collections.Generic;
using Shmup;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    public GameObject[] hearths;
    public GameObject hearthPrefab;
    public PlayerController playerController;

    private bool startNextLevel = false;
    private float nextLevelTimer = 3f;
    private readonly string[] levels = { "Level1", "Level2", "Level3" };
    private int currentLevel = 1;

    public int score = 0;
    private TextMeshProUGUI scoreText;
    public GameOverScreen gameOverScreen;
    public PauseMenu pauseScreen;

    private List<Bullet> bullets = new List<Bullet>();

    private const float InitialHealth = 3f;
    private const float NextLevelDelay = 3f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeReferences();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeReferences()
    {
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        playerController = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
        gameOverScreen = GameObject.FindFirstObjectByType<GameOverScreen>();
        pauseScreen = GameObject.FindFirstObjectByType<PauseMenu>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeReferences();
        if (gameOverScreen != null && gameOverScreen.GameOverPanel.activeSelf)
        {
            gameOverScreen.UnSetup();
        }
        if (pauseScreen != null && pauseScreen.PauseMenuUI.activeSelf)
        {
            pauseScreen.Resume();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (gameOverScreen != null && gameOverScreen.GameOverPanel.activeSelf)
        {
            gameOverScreen.UnSetup();
        }
        if (pauseScreen != null && pauseScreen.PauseMenuUI.activeSelf)
        {
            pauseScreen.Resume();
        }
    }

    private void Update()
    {
        if (playerController == null || playerController.health <= 0)
        {
            ShowGameOverScreen();
            return;
        }

        HandleNextLevelTransition();
        UpdateHealthUI();
    }

    private void ShowGameOverScreen()
    {
        if (gameOverScreen != null && !gameOverScreen.GameOverPanel.activeSelf)
        {
            gameOverScreen.Setup();
        }
    }

    private void HandleNextLevelTransition()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                LoadNextLevel();
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    private void LoadNextLevel()
    {
        currentLevel++;
        if (currentLevel <= levels.Length)
        {
            string sceneName = levels[currentLevel - 1];
            SceneManager.LoadSceneAsync(sceneName);
        }
        nextLevelTimer = NextLevelDelay;
        startNextLevel = false;
    }

    public void OnHealthIncreasePowerUp()
    {
        if (playerController != null)
        {
            playerController.IncreaseHealth(1);
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        float health = playerController.health;
        Debug.Log($"Player Health: {health}");

        for (int i = 0; i < hearths.Length; i++)
        {
            if (playerController.health < i + 1 && hearths[i] != null)
            {
                Destroy(hearths[i].gameObject);
                hearths[i] = null;
            }
            else if (playerController.health >= i + 1 && hearths[i] == null)
            {
                GameObject hearthsParent = GameObject.Find("Hearths");
                if (hearthsParent != null)
                {
                    hearths[i] = Instantiate(hearthPrefab, hearthsParent.transform);
                    RectTransform rt = hearths[i].GetComponent<RectTransform>();
                    if (rt != null)
                    {
                        rt.anchoredPosition = new Vector2(120f + (i * 90f), 0f);
                    }
                }
            }
        }
    }

    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void ResetLevel()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if (playerController != null)
        {
            playerController.health = InitialHealth;
        }

        if (hearths != null)
        {
            foreach (GameObject hearth in hearths)
            {
                if (hearth != null)
                {
                    Destroy(hearth);
                }
            }
        }

        GameObject hearthsParent = GameObject.Find("Hearths");
        if (hearthsParent == null)
        {
            Debug.LogError("Hearths parent object not found!");
            return;
        }

        hearths = new GameObject[(int)InitialHealth];
        hearths[0] = Instantiate(hearthPrefab, hearthsParent.transform);
        hearths[1] = Instantiate(hearthPrefab, hearthsParent.transform);
        hearths[2] = Instantiate(hearthPrefab, hearthsParent.transform);

        RectTransform rt0 = hearths[0].GetComponent<RectTransform>();
        RectTransform rt1 = hearths[1].GetComponent<RectTransform>();
        RectTransform rt2 = hearths[2].GetComponent<RectTransform>();

        if (rt0 != null) rt0.anchoredPosition = new Vector2(120f, 0f);
        if (rt1 != null) rt1.anchoredPosition = new Vector2(210f, 0f);
        if (rt2 != null) rt2.anchoredPosition = new Vector2(300f, 0f);

        foreach (Bullet b in bullets)
        {
            if (b != null)
            {
                Destroy(b.gameObject);
            }
        }
        bullets.Clear();

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        if (gameOverScreen != null && gameOverScreen.GameOverPanel.activeSelf)
        {
            gameOverScreen.UnSetup();
        }
        if (pauseScreen != null && pauseScreen.PauseMenuUI.activeSelf)
        {
            pauseScreen.Resume();
        }
    }

    public void AddBullet(Bullet newBullet)
    {
        bullets.Add(newBullet);
    }
    public void OnBossDefeated()
    {
        startNextLevel = true;
        nextLevelTimer = NextLevelDelay;
    }
}
