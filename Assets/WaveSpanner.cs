using UnityEngine;

namespace Shmup
{
    public class WaveSpanner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn; // Spawnlanacak prefab
        [SerializeField] private float spawnInterval = 5f; // Spawn aralýðý (saniye)
        [SerializeField] private float moveSpeed = 2f; // Hareket hýzý
        [SerializeField] private float amplitude = 2f; // Sinüs dalgasý genliði
        [SerializeField] private float frequency = 2f; // Sinüs dalgasý frekansý

        private float spawnTimer;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            spawnTimer = spawnInterval; // Ýlk spawn için zamanlayýcýyý baþlat
        }

        // Update is called once per frame
        void Update()
        {
            if (BossStage.Instance != null && BossStage.Instance.bossSpawned)
            {
                return; // Boss spawnlandýktan sonra prefab spawnlanmasýný durdur
            }

            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnPrefab();
                spawnTimer = spawnInterval; // Zamanlayýcýyý sýfýrla
            }
        }

        private void SpawnPrefab()
        {
            if (prefabToSpawn != null)
            {
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
                Debug.Log("Prefab spawnlandý!");

                // MoveSin bileþenini ekle ve ayarlarýný yap
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
