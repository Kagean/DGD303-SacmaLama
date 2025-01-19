using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public float health = 3f; // Büyük meteorun başlangıç sağlığı
    public GameObject[] smallMeteorPrefabs; // Farklı küçük meteor prefab'ları
    public int smallMeteorCount = 3; // Oluşacak küçük meteor sayısı
    public float spawnRadius = 0.5f; // Küçük meteorlardan oluşacak yarıçap
    public float minSpeed = 2f; // Küçük meteorlara minimum hız
    public float maxSpeed = 3f; // Küçük meteorlara maksimum hız
    public float scatterAngle = 45f; // Saçılma yönü açısı (sol tarafa eğilimli)
    public GameObject explosionPrefab;

    void Start()
    {
        Level.Instance.AddDestroy(); // Level sistemine bu meteoru ekler
    }

    void Update()
    {
        canBeDestroyed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy)
        {
            TakeDamage(bullet.damage); // Hasar uygula
            Destroy(bullet.gameObject); // Mermiyi yok et

            if (health <= 0)
            {
                Level.Instance.AddScore(scoreValue); // Skor ekle
                Explode(); // Patlama ve küçük meteor oluşturma
                Destroy(gameObject); // Büyük meteoru yok et
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Explode()
    {
        // Patlama animasyonunu oluştur
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

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

            SmallMeteor smallMeteorScript = smallMeteor.GetComponent<SmallMeteor>();
            if (smallMeteorScript != null)
            {
                smallMeteorScript.health = 1f;
                smallMeteorScript.scoreValue = 50;
            }
        }
    }



    private void OnDestroy()
    {
        Level.Instance.RemoveDestroy(); // Level sisteminden bu meteoru kaldır
    }
}
