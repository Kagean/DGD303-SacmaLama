using UnityEngine;

public class Destroy : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public float health = 1f; // Düþmanýn saðlýðý

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        canBeDestroyed = true;
        Attack[] attacks = transform.GetComponentsInChildren<Attack>();
        foreach (Attack attack in attacks)
        {
            attack.isActive = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                // Merminin hasarýný uygula
                TakeDamage(bullet.damage); // Bullet'ýn damage'ýný düþmana uygula
                Destroy(bullet.gameObject); // Mermiyi yok et

                // Eðer düþman öldü ise, puan ekle ve düþmaný yok et
                if (health <= 0)
                {
                    Level.Instance.AddScore(scoreValue);
                    Destroy(gameObject); // Düþmaný yok et
                }
            }
        }
    }

    // Düþmana hasar uygulama fonksiyonu
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

}
