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
                    // Saldýrý yapýldýktan sonra bu eþiði tekrar kontrol etmemek için eþiði büyük bir deðere ayarlayalým
                    healthThresholds[i].Threshold = int.MaxValue;
                }
            }
        }

        // Caný azaltmak için bir metod ekleyelim
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"Boss hasar aldý: {damage}, kalan can: {currentHealth}"); // Hasar alýndýðýný doðrulamak için log ekleyelim
            if (currentHealth <= 0)
            {
                Die();
                Destroy(gameObject);
            }
        }

        private void Die()
        {
            // Boss öldüðünde yapýlacak iþlemler burada olacak
            Debug.Log("Boss öldü!");
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
                // Belirtilen saldýrý metodunu çaðýr
                Debug.Log($"Performing attack: {method}");
                // Bu örnekte sadece log yazýyoruz, gerçek saldýrý metodlarýný çaðýrmak için Invoke kullanýlabilir
                // Invoke(method, 0f);
            }
        }
    }
}
