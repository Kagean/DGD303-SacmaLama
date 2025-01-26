using Shmup;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public bool isBigMeteor = false; // Meteor türünü belirler
    public int scoreValue = 50; // Yok edildiğinde kazandıracağı skor
    public float health = 1f; // Meteorun başlangıç sağlığı
    public GameObject explosionPrefab; // Patlama animasyonu prefab'i
    public GameObject[] smallMeteorPrefabs; // Küçük meteor prefab'ları (büyük meteorlar için)
    public int smallMeteorCount = 3; // Büyük meteor patladığında kaç küçük meteor çıkacak
    public float spawnRadius = 0.5f; // Küçük meteorların çıkacağı yarıçap
    public float minSpeed = 2f; // Küçük meteorlara minimum hız
    public float maxSpeed = 3f; // Küçük meteorlara maksimum hız
    public float scatterAngle = 45f; // Küçük meteorlara saçılma açısı (sol tarafa eğilimli)
    private bool DamagePlayer = false;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy)
        {
            TakeDamage(bullet.damage); // Hasar uygula
            Destroy(bullet.gameObject); // Mermiyi yok et
            return; // Çifte işlemden kaçınmak için return
        }

        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && !DamagePlayer)
        {
            DamagePlayer = true; // Çifte hasarı engelle
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Sağlığı azalt
        if (health <= 0)
        {
            Explode(); // Meteor patlama
        }
    }

    public void Explode()
    {
        if (isBigMeteor)
        {
            SpawnSmallMeteors(); // Küçük meteorları oluştur
        }

        // Patlama animasyonu oluştur
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Meteoru yok et
    }

    private void SpawnSmallMeteors()
    {
        for (int i = 0; i < smallMeteorCount; i++)
        {
            GameObject randomSmallMeteor = smallMeteorPrefabs[Random.Range(0, smallMeteorPrefabs.Length)];
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
            GameObject smallMeteor = Instantiate(randomSmallMeteor, spawnPosition, Quaternion.identity);

            // Küçük meteorlara hız ve yön ekle
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
}
