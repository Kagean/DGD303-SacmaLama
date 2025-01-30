using UnityEngine;

namespace Shmup
{
    public class MoveSin : MonoBehaviour
    {
        float sinCenterY;
        public float amplitude = 2;
        public float frequency = 2;
        public float speed = 2; // Hýz deðiþkeni

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            sinCenterY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Vector2 pos = transform.position;

            // X pozisyonunu hýzla çarparak güncelle
            pos.x -= speed * Time.fixedDeltaTime;

            // Sinüs dalgasý hareketi
            float sin = Mathf.Sin(pos.x * frequency) * amplitude;
            pos.y = sinCenterY + sin;

            transform.position = pos;
        }
    }
}
