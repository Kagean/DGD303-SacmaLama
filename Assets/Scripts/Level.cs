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
    int currentlevel = 1;

    private void Awake()
    {

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
                nextLevelTimer = 3;
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
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy (b.gameObject);
        }
        numDestroies = 0;
        string sceneName = levels[currentlevel - 1];
        SceneManager.LoadScene(sceneName);
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
