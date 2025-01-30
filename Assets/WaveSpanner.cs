using UnityEngine;

namespace Shmup
{
    public class WaveSpanner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn; // Spawnlanacak prefab
        [SerializeField] private float spawnInterval = 5f; // Spawn aral��� (saniye)
        [SerializeField] private float moveSpeed = 2f; // Hareket h�z�
        [SerializeField] private float amplitude = 2f; // Sin�s dalgas� genli�i
        [SerializeField] private float frequency = 2f; // Sin�s dalgas� frekans�

        private float spawnTimer;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            spawnTimer = spawnInterval; // �lk spawn i�in zamanlay�c�y� ba�lat
        }

        // Update is called once per frame
        void Update()
        {
            if (BossStage.Instance != null && BossStage.Instance.bossSpawned)
            {
                return; // Boss spawnland�ktan sonra prefab spawnlanmas�n� durdur
            }

            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnPrefab();
                spawnTimer = spawnInterval; // Zamanlay�c�y� s�f�rla
            }
        }

        private void SpawnPrefab()
        {
            if (prefabToSpawn != null)
            {
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
                Debug.Log("Prefab spawnland�!");

                // MoveSin bile�enini ekle ve ayarlar�n� yap
                MoveSin moveSin = spawnedPrefab.AddComponent<MoveSin>();
                moveSin.speed = moveSpeed;
                moveSin.amplitude = amplitude;
                moveSin.frequency = frequency;
            }
            else
            {
                Debug.LogWarning("Prefab ayarlanmam��.");
            }
        }
    }
}
