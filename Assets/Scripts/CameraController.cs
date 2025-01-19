using UnityEngine;

namespace Shmup
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] float speed = 2f;

        public float Speed => isMoving ? speed : 0f; // Eğer hareket durdurulmuşsa hız sıfır olur

        private bool isMoving = true; // Kamera hareket kontrolü

        void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        private void LateUpdate()
        {
            if (isMoving) // Eğer kamera hareket ediyorsa
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }

        public void StopMoving()
        {
            isMoving = false; // Kamerayı durdur
        }

        public void StartMoving()
        {
            isMoving = true; // Kamerayı tekrar başlat
        }
    }
}
