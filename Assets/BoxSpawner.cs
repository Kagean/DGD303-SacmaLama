using UnityEngine;
using System.Linq;

namespace Shmup
{
    public class BoxSpawner : MonoBehaviour
    {
        public GameObject[] boxPrefabs; // Kutularýn prefab'larý
        public float minSpawnTime = 1f; // Minimum spawn süresi
        public float maxSpawnTime = 5f; // Maksimum spawn süresi
        private float nextSpawnTime;
        private System.Random random = new System.Random(); // Random örneði

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ScheduleNextSpawn();
        }

        // Update is called once per frame
        void Update()
        {
            if (BossStage.Instance != null && BossStage.Instance.bossSpawned)
            {
                return; // Boss spawnlandýktan sonra kutu spawnlanmasýný durdur
            }

            if (Time.time >= nextSpawnTime)
            {
                SpawnBoxes();
                ScheduleNextSpawn();
            }
        }

        private void ScheduleNextSpawn()
        {
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }

        private void SpawnBoxes()
        {
            // 3 kutudan rastgele 1 tanesini seç
            int index = random.Next(boxPrefabs.Length);
            GameObject box = Instantiate(boxPrefabs[index], transform.position, Quaternion.identity);
            box.transform.SetParent(transform); // Kutuyu spawner'ýn çocuðu olarak ayarla
            Destroy(box, 10f); // 10 saniye sonra yok et
        }
    }
}
