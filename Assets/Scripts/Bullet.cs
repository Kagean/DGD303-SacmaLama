using Shmup;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public int damage = 1; // Merminin verece�i hasar miktar�
    public Vector2 velocity;
    public bool isEnemy = false;
    public bool isBossWeapon = false; // Bu merminin boss silah� olup olmad���n� kontrol eder

    void Start()
    {
        Destroy(gameObject, 4); // Mermiyi 4 saniye sonra yok et
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

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }


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

        // E�er �arp�lan nesne boss ise ve bu mermi boss silah� de�ilse, hasar ver
        if (other.CompareTag("Boss") && !isBossWeapon)
        {
            // Boss nesnesinin health script'ine eri�erek hasar uygula
            Boss bossHealth = other.GetComponent<Boss>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
