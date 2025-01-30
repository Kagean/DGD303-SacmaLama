using UnityEngine;

namespace Shmup
{
    public class TargetLock : MonoBehaviour
    {
        public Transform player; // Oyuncunun Transform bile�eni
        public float speed = 5f; // D��man�n hareket h�z�

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
                // Oyuncuya do�ru hareket et
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
