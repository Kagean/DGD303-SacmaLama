using UnityEngine;

namespace Shmup
{
    public class Boss : MonoBehaviour
    {
        public float bosshealth = 10f; 
        public BossWeapon[] weapons; 
        public float activateWeaponsHealthThreshold = 50f; 

        void Update()
        {

            if (bosshealth <= activateWeaponsHealthThreshold)
            {
                ActivateWeapons();
            }
        }

        public void TakeDamage(float damage)
        {
            bosshealth -= damage;
            if (bosshealth <= 0)
            {
                Die();
            }
        }

        private void ActivateWeapons()
        {
            foreach (var weapon in weapons)
            {
                weapon.Activate();
            }
        }

        private void Die()
        {

            Debug.Log("Boss öldü!");
            if (Level.Instance != null)
            {
                Level.Instance.AddScore(2000);
                Level.Instance.OnBossDefeated();
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && !bullet.isEnemy && !bullet.isBossWeapon)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
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
                // Belirtilen saldırı metodunu çağır
                Debug.Log($"Performing attack: {method}");
                // Bu örnekte sadece log yazıyoruz, gerçek saldırı metodlarını çağırmak için Invoke kullanılabilir
                // Invoke(method, 0f);
            }
        }
    }
    public class BossController : MonoBehaviour
    {
        public void OnDestroy()
        {
            if (Level.Instance != null)
            {
                Level.Instance.OnBossDefeated();
            }
        }
    }
}