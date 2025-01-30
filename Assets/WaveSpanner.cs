using UnityEngine;

namespace Shmup
{
    public class WaveSpanner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float amplitude = 2f;
        [SerializeField] private float frequency = 2f;

        private float spawnTimer;

        void Start()
        {
            spawnTimer = spawnInterval;
        }

        void Update()
        {
            if (BossStage.Instance != null && BossStage.Instance.bossSpawned)
            {
                return;
            }

            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnPrefab();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnPrefab()
        {
            if (prefabToSpawn != null)
            {
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
                Debug.Log("Prefab spawnlandý!");


                MoveSin moveSin = spawnedPrefab.AddComponent<MoveSin>();
                moveSin.speed = moveSpeed;
                moveSin.amplitude = amplitude;
                moveSin.frequency = frequency;
            }
            else
            {
                Debug.LogWarning("Prefab ayarlanmamýþ.");
            }
        }
    }
}
