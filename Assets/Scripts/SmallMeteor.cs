using UnityEngine;

public class SmallMeteor : MonoBehaviour
{
    public int scoreValue = 50;
    public float health = 1f;
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.isEnemy)
        {
            TakeDamage(bullet.damage);
            Destroy(bullet.gameObject);

            if (health <= 0)
            {
                Level.Instance.AddScore(scoreValue);
                Destroy(gameObject); // Küçük meteoru yok et
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    private void OnDestroy()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

}
