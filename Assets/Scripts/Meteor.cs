using Shmup;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public bool isMediumBird = false;
    public bool isBox = false;
    public bool isBigMeteor = false;
    public int scoreValue = 50;
    public float health = 1f;
    public GameObject explosionPrefab;
    public GameObject[] smallMeteorPrefabs;
    public int smallMeteorCount = 3;
    public float spawnRadius = 0.5f;
    public float minSpeed = 2f;
    public float maxSpeed = 3f;
    public float scatterAngle = 45f;
    private bool DamagePlayer = false;

    private void Start()
    {
        if (isMediumBird)
        {
            Invoke("ExplodeAndDestroy", 7);
        }

        if (isBox)
        {
            Invoke("ExplodeAndDestroy", 15);
        }
    }

    private void ExplodeAndDestroy()
    {
        Explode();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy)
        {
            TakeDamage(bullet.damage);
            Destroy(bullet.gameObject);
            return;
        }

        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && !DamagePlayer)
        {
            DamagePlayer = true;
            if (isMediumBird)
            {
                Explode();
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (isBigMeteor)
        {
            SpawnSmallMeteors();
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Level.Instance.AddScore(scoreValue);

        Destroy(gameObject);
    }

    private void SpawnSmallMeteors()
    {
        for (int i = 0; i < smallMeteorCount; i++)
        {
            GameObject randomSmallMeteor = smallMeteorPrefabs[Random.Range(0, smallMeteorPrefabs.Length)];
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
            GameObject smallMeteor = Instantiate(randomSmallMeteor, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = smallMeteor.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randomAngle = Random.Range(-scatterAngle, scatterAngle);
                Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.left;
                float randomSpeed = Random.Range(minSpeed, maxSpeed);
                rb.linearVelocity = direction * randomSpeed;
            }
        }
    }
    private void OnDestroy()
    {
        if (BossStage.Instance != null)
        {
            BossStage.Instance.OnEnemyKilled();
        }
    }

}
