using UnityEngine;

namespace Shmup
{
    public class TargetLock : MonoBehaviour
    {
        public Transform player; // Oyuncunun Transform bileþeni
        public float speed = 5f; // Düþmanýn hareket hýzý

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player").transform; // Oyuncuyu bul ve ata
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null)
            {
                // Oyuncuya doðru hareket et
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
