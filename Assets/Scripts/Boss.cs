using UnityEngine;

namespace Shmup
{
    public class Boss : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 500;
        private int currentHealth;

        [SerializeField]
        private HealthThreshold[] healthThresholds;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < healthThresholds.Length; i++)
            {
                if (currentHealth <= healthThresholds[i].Threshold)
                {
                    healthThresholds[i].PerformAttacks();
                    // Sald�r� yap�ld�ktan sonra bu e�i�i tekrar kontrol etmemek i�in e�i�i b�y�k bir de�ere ayarlayal�m
                    healthThresholds[i].Threshold = int.MaxValue;
                }
            }
        }

        // Can� azaltmak i�in bir metod ekleyelim
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"Boss hasar ald�: {damage}, kalan can: {currentHealth}"); // Hasar al�nd���n� do�rulamak i�in log ekleyelim
            if (currentHealth <= 0)
            {
                Die();
                Destroy(gameObject);
            }
        }

        private void Die()
        {
            // Boss �ld���nde yap�lacak i�lemler burada olacak
            Debug.Log("Boss �ld�!");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null && !bullet.isEnemy) // Sadece oyuncu mermileri hasar verebilir
            {
                TakeDamage(bullet.damage);
                Destroy(bullet.gameObject);
            }
        }
    }

    [System.Serializable]
    public class HealthThreshold
    {
        public int Threshold;
        public string[] AttackMethods;

        public void PerformAttacks()
        {
            foreach (var method in AttackMethods)
            {
                // Belirtilen sald�r� metodunu �a��r
                Debug.Log($"Performing attack: {method}");
                // Bu �rnekte sadece log yaz�yoruz, ger�ek sald�r� metodlar�n� �a��rmak i�in Invoke kullan�labilir
                // Invoke(method, 0f);
            }
        }
    }
}
