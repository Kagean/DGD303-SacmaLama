using UnityEngine;

namespace Shmup
{
    public class BossStage : MonoBehaviour
    {
        public static BossStage Instance { get; private set; } // Singleton instance

        public GameObject bossPrefab; // Boss prefab'�
        public Transform bossSpawnPoint; // Boss'un ortaya ��kaca�� nokta
        public int enemiesToKillForBoss = 10; // Boss'un ortaya ��kmas� i�in �ld�r�lmesi gereken d��man say�s�
        private int enemiesKilled = 0; // �ld�r�len d��man say�s�
        public bool bossSpawned { get; private set; } = false; // Boss'un spawnlan�p spawnlanmad���n� kontrol eden de�i�ken

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // D��man �ld�r�ld���nde �a�r�lacak y�ntem
        public void OnEnemyKilled()
        {
            if (bossSpawned)
            {
                return; // Boss zaten spawnlanm��sa metottan ��k
            }

            enemiesKilled++;
            Debug.Log($"�ld�r�len d��man say�s�: {enemiesKilled}/{enemiesToKillForBoss}");
            if (enemiesKilled >= enemiesToKillForBoss)
            {
                SpawnBoss();
            }
        }

        // Boss'u ortaya ��karan y�ntem
        private void SpawnBoss()
        {
            if (bossSpawned)
            {
                return; // Boss zaten spawnlanm��sa metottan ��k
            }

            if (bossPrefab != null && bossSpawnPoint != null)
            {
                GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
                boss.transform.SetParent(bossSpawnPoint);
                bossSpawned = true; // Boss'un spawnland���n� belirt
                Debug.Log("Boss ortaya ��kt�!");
            }
            else
            {
                Debug.LogWarning("Boss prefab veya spawn noktas� ayarlanmam��.");
            }
        }
    }
}
