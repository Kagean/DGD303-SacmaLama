using Shmup;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public int damage = 1; // Merminin vereceði hasar miktarý
    public Vector2 velocity;
    public bool isEnemy = false;
    public bool isBossWeapon = false; // Bu merminin boss silahý olup olmadýðýný kontrol eder

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

    // Merminin bir nesneye çarpmasýný kontrol et
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }


        // Eðer çarpýlan nesne düþman ise ve mermi düþman mermisi deðilse, hasar ver
        if (other.CompareTag("Enemy") && !isEnemy)
        {
            // Düþman nesnesinin health script'ine eriþerek hasar uygula
            Destroy enemyHealth = other.GetComponent<Destroy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Mermiyi yok et
        }

        // Eðer çarpýlan nesne boss ise ve bu mermi boss silahý deðilse, hasar ver
        if (other.CompareTag("Boss") && !isBossWeapon)
        {
            // Boss nesnesinin health script'ine eriþerek hasar uygula
            Boss bossHealth = other.GetComponent<Boss>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
