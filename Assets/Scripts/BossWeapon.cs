using UnityEngine;
using System.Collections;

namespace Shmup
{
    public class BossWeapon : MonoBehaviour
    {
        public Bullet missilePrefab; // Füze prefab'ý
        public Transform playerTransform; // Oyuncunun transformu
        public float missileShootIntervalSeconds = 1.0f; // Misilleme saldýrýlarý arasýndaki süre
        private float missileShootTimer = 0.0f; // Misilleme saldýrýsý zamanlayýcýsý
        public bool isActive = false; // Silahýn aktif olup olmadýðýný kontrol eder

        void Update()
        {
            if (!isActive || playerTransform == null)
                return;

            missileShootTimer += Time.deltaTime;
            if (missileShootTimer >= missileShootIntervalSeconds)
            {
                MissileAttack(playerTransform);
                missileShootTimer = 0.0f; // Zamanlayýcýyý sýfýrla
            }
        }

        public void MissileAttack(Transform targetTransform)
        {
            GameObject go = Instantiate(missilePrefab.gameObject, transform.position, Quaternion.identity);
            Bullet missile = go.GetComponent<Bullet>();
            missile.isEnemy = true; // Merminin düþman mermisi olduðunu belirt
            missile.isBossWeapon = true; // Merminin boss silahý olduðunu belirt
            StartCoroutine(UpdateMissileDirection(missile, targetTransform));
        }

        private IEnumerator UpdateMissileDirection(Bullet missile, Transform targetTransform)
        {
            while (missile != null && targetTransform != null)
            {
                missile.direction = (targetTransform.position - missile.transform.position).normalized;
                yield return null; // Bir sonraki kareye kadar bekle
            }
        }

        public void Activate()
        {
            isActive = true;
            Debug.Log($"Boss weapon activated: {gameObject.name}");
        }

        public void Deactivate()
        {
            isActive = false;
            Debug.Log($"Boss weapon deactivated: {gameObject.name}");
        }
    }
}
