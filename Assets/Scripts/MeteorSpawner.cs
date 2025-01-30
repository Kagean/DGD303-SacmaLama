using UnityEngine;

namespace Shmup
{
    public class MeteorSpawner : MonoBehaviour
    {
        public GameObject meteorPrefab;
        public float spawnInterval1 = 2.0f;
        public float spawnInterval2 = 5.0f;

        private float nextSpawnTime1;
        private float nextSpawnTime2;

        void Start()
        {
            nextSpawnTime1 = Time.time + spawnInterval1;
            nextSpawnTime2 = Time.time + spawnInterval2;
        }


        void Update()
        {
            if (BossStage.Instance != null && BossStage.Instance.bossSpawned)
            {
                return;
            }

            if (Time.time >= nextSpawnTime1)
            {
                SpawnMeteor();
                nextSpawnTime1 = Time.time + spawnInterval1;
            }

            if (Time.time >= nextSpawnTime2)
            {
                SpawnMeteor();
                nextSpawnTime2 = Time.time + spawnInterval2;
            }
        }

        void SpawnMeteor()
        {
            Instantiate(meteorPrefab, transform.position, transform.rotation);
        }
    }
}
