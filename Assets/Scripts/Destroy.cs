using UnityEngine;

public class Destroy : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public float health = 1f; // D��man�n sa�l���

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
                // Merminin hasar�n� uygula
                TakeDamage(bullet.damage); // Bullet'�n damage'�n� d��mana uygula
                Destroy(bullet.gameObject); // Mermiyi yok et

                // E�er d��man �ld� ise, puan ekle ve d��man� yok et
                if (health <= 0)
                {
                    Level.Instance.AddScore(scoreValue);
                    Destroy(gameObject); // D��man� yok et
                }
            }
        }
    }

    // D��mana hasar uygulama fonksiyonu
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

}
