using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static Level Instance;

    uint numDestroies = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Level1", "Level2" };
    int curentlevel = 1;

    int scorePoint = 0;
    Text score;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            score = GameObject.Find("Score point").GetComponent<Text>();
        }
        else 
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                curentlevel++;
                if (curentlevel <= levels.Length)
                {
                    string scenename = levels[curentlevel - 1];
                    SceneManager.LoadSceneAsync(scenename);
                }
                else
                {
                    Debug.Log("Game Over!!!");
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    public void AddScore(int amountToadd)
    {
        scorePoint += amountToadd;
        score.text = scorePoint.ToString();
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
