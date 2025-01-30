using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public int damage = 1; // Merminin verece�i hasar miktar�

    public Vector2 velocity;

    public bool isEnemy = false;

    void Start()
    {
        Destroy(gameObject, 3); // Mermiyi 3 saniye sonra yok et
    }

    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }

    // Merminin bir nesneye �arpmas�n� kontrol et
    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er �arp�lan nesne d��man ise ve mermi d��man mermisi de�ilse, hasar ver
        if (other.CompareTag("Enemy") && !isEnemy)
        {
            // D��man nesnesinin health script'ine eri�erek hasar uygula
            Destroy enemyHealth = other.GetComponent<Destroy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
