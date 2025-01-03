using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Destroy : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreValue = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Level.Instance.AddDestroy();
    }

    // Update is called once per frame
    void Update()
    {


        canBeDestroyed = true;
            Attack[] attacks = transform.GetComponentsInChildren<Attack>();
            foreach (Attack attack in attacks)
            {
                attack.isActive = true;
            }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                Level.Instance.AddScore(scoreValue);
                Level.Instance.RemoveDestroy();
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }
}
