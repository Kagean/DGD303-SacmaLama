using UnityEngine;

namespace Shmup
{
    public class BossStage : MonoBehaviour
    {
        public static BossStage Instance { get; private set; } // Singleton instance

        public GameObject bossPrefab; // Boss prefab'ý
        public Transform bossSpawnPoint; // Boss'un ortaya çýkacaðý nokta
        public int enemiesToKillForBoss = 10; // Boss'un ortaya çýkmasý için öldürülmesi gereken düþman sayýsý
        private int enemiesKilled = 0; // Öldürülen düþman sayýsý
        public bool bossSpawned { get; private set; } = false; // Boss'un spawnlanýp spawnlanmadýðýný kontrol eden deðiþken

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

        // Düþman öldürüldüðünde çaðrýlacak yöntem
        public void OnEnemyKilled()
        {
            if (bossSpawned)
            {
                return; // Boss zaten spawnlanmýþsa metottan çýk
            }

            enemiesKilled++;
            Debug.Log($"Öldürülen düþman sayýsý: {enemiesKilled}/{enemiesToKillForBoss}");
            if (enemiesKilled >= enemiesToKillForBoss)
            {
                SpawnBoss();
            }
        }

        // Boss'u ortaya çýkaran yöntem
        private void SpawnBoss()
        {
            if (bossSpawned)
            {
                return; // Boss zaten spawnlanmýþsa metottan çýk
            }

            if (bossPrefab != null && bossSpawnPoint != null)
            {
                GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
                boss.transform.SetParent(bossSpawnPoint);
                bossSpawned = true; // Boss'un spawnlandýðýný belirt
                Debug.Log("Boss ortaya çýktý!");
            }
            else
            {
                Debug.LogWarning("Boss prefab veya spawn noktasý ayarlanmamýþ.");
            }
        }
    }
}
