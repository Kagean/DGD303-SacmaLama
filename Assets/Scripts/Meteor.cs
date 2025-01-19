using UnityEngine;

public class Meteor : MonoBehaviour
{
    public bool isBigMeteor = false; // Meteor türünü belirle
    public int scoreValue = 50; // Yok edildiğinde kazandıracağı skor
    public GameObject explosionPrefab; // Patlama animasyonu prefab'i
    public GameObject[] smallMeteorPrefabs; // Küçük meteor prefab'ları
    public int smallMeteorCount = 3; // Büyük meteor patladığında kaç küçük meteor çıkacak
    public float spawnRadius = 1f; // Küçük meteorların çıkacağı yarıçap
    public float speed = 5f; // Küçük meteorlara hız eklemek için

    private int health; // Meteorun canı

    private void Awake()
    {
        // Meteor türüne göre sağlığı ayarla
        if (isBigMeteor)
        {
            health = 3; // Büyük meteorun canı
        }
        else
        {
            health = 1; // Küçük meteorun canı
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy)
        {
            TakeDamage(bullet.damage); // Hasar al
            Destroy(bullet.gameObject); // Mermiyi yok et
        }

        // Oyuncuya hasar verme (big ve small meteorlara çarptığında)
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.health -= 1; // Oyuncuya 1 hasar ver
        }
    }

    public void TakeDamage(float damage)
    {
        health -= (int)damage; // Sağlık değerini int'e çevirip azalt
        if (health <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        // Eğer büyük bir meteor ise, küçük meteorlardan oluştur
        if (isBigMeteor)
        {
            for (int i = 0; i < smallMeteorCount; i++)
            {
                GameObject randomSmallMeteor = smallMeteorPrefabs[Random.Range(0, smallMeteorPrefabs.Length)];
                Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
                GameObject smallMeteor = Instantiate(randomSmallMeteor, spawnPosition, Quaternion.identity);

                // Küçük meteorlara hız ekleyelim (Sola doğru)
                Rigidbody2D rb = smallMeteor.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 randomDirection = new Vector2(-1f, Random.Range(-0.5f, 0.5f)).normalized; // Yalnızca sola doğru yön
                    rb.linearVelocity = randomDirection * speed; // Yön ve hız
                }
            }
        }

        // Patlama animasyonu oluştur
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Meteor yok ol
    }
}
