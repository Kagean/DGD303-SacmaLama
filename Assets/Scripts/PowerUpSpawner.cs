using System.Collections;
using System.Linq;
using UnityEngine;

namespace Shmup
{
    [System.Serializable]
    public class PowerUpEntry
    {
        public GameObject prefab;
        public float weight;
    }

    public class PowerUpSpawner : MonoBehaviour
    {
        public PowerUpEntry[] powerUps;
        public Transform spawnPoint;
        public float spawnInterval = 20f;

        private void Start()
        {
            StartCoroutine(SpawnPowerUps());
        }

        private IEnumerator SpawnPowerUps()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnPowerUp();
            }
        }

        private void SpawnPowerUp()
        {
            if (spawnPoint == null || powerUps.Length == 0)
            {
                return;
            }

            GameObject selectedPowerUp = GetRandomPowerUp();
            Instantiate(selectedPowerUp, spawnPoint.position, spawnPoint.rotation);
        }

        private GameObject GetRandomPowerUp()
        {
            float totalWeight = powerUps.Sum(p => p.weight);
            float randomValue = Random.Range(0, totalWeight);

            foreach (var powerUp in powerUps)
            {
                if (randomValue < powerUp.weight)
                {
                    return powerUp.prefab;
                }
                randomValue -= powerUp.weight;
            }

            return powerUps[0].prefab;
        }
    }
}
