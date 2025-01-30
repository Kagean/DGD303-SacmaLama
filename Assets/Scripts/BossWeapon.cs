using UnityEngine;
using System.Collections;

namespace Shmup
{
    public class BossWeapon : MonoBehaviour
    {
        public Bullet missilePrefab; // F�ze prefab'�
        public Transform playerTransform; // Oyuncunun transformu
        public float missileShootIntervalSeconds = 1.0f; // Misilleme sald�r�lar� aras�ndaki s�re
        private float missileShootTimer = 0.0f; // Misilleme sald�r�s� zamanlay�c�s�
        public bool isActive = false; // Silah�n aktif olup olmad���n� kontrol eder

        void Update()
        {
            if (!isActive || playerTransform == null)
                return;

            missileShootTimer += Time.deltaTime;
            if (missileShootTimer >= missileShootIntervalSeconds)
            {
                MissileAttack(playerTransform);
                missileShootTimer = 0.0f; // Zamanlay�c�y� s�f�rla
            }
        }

        public void MissileAttack(Transform targetTransform)
        {
            GameObject go = Instantiate(missilePrefab.gameObject, transform.position, Quaternion.identity);
            Bullet missile = go.GetComponent<Bullet>();
            missile.isEnemy = true; // Merminin d��man mermisi oldu�unu belirt
            missile.isBossWeapon = true; // Merminin boss silah� oldu�unu belirt
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
