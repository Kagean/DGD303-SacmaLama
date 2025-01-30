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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ScheduleNextSpawn();
        }

        // Update is called once per frame
        void Update()
        {
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
            // 3 kutudan rastgele 2'sini seç
            int[] indices = { 0, 1, 2 };
            System.Random random = new System.Random();
            indices = indices.OrderBy(x => random.Next()).ToArray();

            for (int i = 0; i < 2; i++)
            {
                GameObject box = Instantiate(boxPrefabs[indices[i]], transform.position, Quaternion.identity);
                Destroy(box, 10f); // 10 saniye sonra yok et
            }
        }
    }
}
