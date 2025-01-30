using Shmup;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public int damage = 1;
    public Vector2 velocity;
    public bool isEnemy = false;
    public bool isBossWeapon = false;

    void Start()
    {
        Destroy(gameObject, 4);
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


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }



        if (other.CompareTag("Enemy") && !isEnemy)
        {

            Destroy enemyHealth = other.GetComponent<Destroy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }


        if (other.CompareTag("Boss") && !isBossWeapon)
        {

            Boss bossHealth = other.GetComponent<Boss>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
