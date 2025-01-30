using UnityEngine;

namespace Shmup
{
    public class Boss : MonoBehaviour
    {
        public float bosshealth = 10f; // Boss'un başlangıç sağlığı
        public BossWeapon[] weapons; // Boss'un sahip olduğu silahlar
        public float activateWeaponsHealthThreshold = 50f; // Silahların etkinleşeceği sağlık eşiği

        void Update()
        {
            // Sağlık belirli bir seviyenin altına düştüğünde silahları etkinleştir
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
            // Boss'un ölme işlemleri burada yapılabilir
            Debug.Log("Boss öldü!");
            Destroy(gameObject);
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
}