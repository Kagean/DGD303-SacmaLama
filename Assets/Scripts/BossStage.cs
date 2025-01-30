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
            if (bossPrefab != null && bossSpawnPoint != null)
            {
                GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
                boss.transform.SetParent(bossSpawnPoint);
                Debug.Log("Boss ortaya çýktý!");
            }
        }
    }
}
