using UnityEngine;
using System.Collections;

namespace Shmup
{
    public class BossWeapon : MonoBehaviour
    {
        public Bullet missilePrefab;
        public Transform playerTransform;
        public float missileShootIntervalSeconds = 1.0f;
        private float missileShootTimer = 0.0f;
        public bool isActive = false;

        void Start()
        {
            // Player tagli GameObject'i bul ve playerTransform olarak ayarla
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        void Update()
        {
            if (isActive && playerTransform != null)
            {
                missileShootTimer += Time.deltaTime;
                if (missileShootTimer >= missileShootIntervalSeconds)
                {
                    MissileAttack(playerTransform);
                    missileShootTimer = 0.0f;
                }
            }
        }

        public void MissileAttack(Transform targetTransform)
        {
            Bullet missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            StartCoroutine(UpdateMissileDirection(missile, targetTransform));
        }

        private IEnumerator UpdateMissileDirection(Bullet missile, Transform targetTransform)
        {
            while (missile != null && targetTransform != null)
            {
                Vector3 direction = (targetTransform.position - missile.transform.position).normalized;
                missile.velocity = direction * missile.speed;
                yield return null;
            }
        }

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }
    }
}
