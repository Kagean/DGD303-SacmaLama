using UnityEngine;

namespace Shmup
{
    public class MeteorSpawner : MonoBehaviour
    {
        public GameObject meteorPrefab; // Meteor prefab referansý
        public float spawnInterval1 = 2.0f; // Ýlk zaman aralýðý
        public float spawnInterval2 = 5.0f; // Ýkinci zaman aralýðý

        private float nextSpawnTime1;
        private float nextSpawnTime2;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            nextSpawnTime1 = Time.time + spawnInterval1;
            nextSpawnTime2 = Time.time + spawnInterval2;
        }

        // Update is called once per frame
        void Update()
        {
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
