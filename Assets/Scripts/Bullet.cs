using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public int damage = 1; // Merminin vereceði hasar

    public Vector2 velocity;

    public bool isEnemy = false;

    void Start()
    {
        Destroy(gameObject, 3); // Mermi 3 saniye sonra yok olur
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
        // Eðer çarpýlan nesne düþman ise, hasar ver
        if (other.CompareTag("Enemy"))
        {
            // Düþman nesnesinin health script'ine eriþerek hasar uygulayýn
            Destroy enemyHealth = other.GetComponent<Destroy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
