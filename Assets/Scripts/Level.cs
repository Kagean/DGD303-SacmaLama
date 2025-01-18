using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    public static Level Instance;

    uint numDestroies = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 5;

    string[] levels = { "Level1", "Level2" };
    int currentlevel = 1;

    int score = 0;
    Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {

    }
    void Update()
    {
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
                }
                nextLevelTimer = 5;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    public void ResetLevel()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        numDestroies = 0;
        string sceneName = levels[currentlevel - 1];
        SceneManager.LoadScene(sceneName);
    }

    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();
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
