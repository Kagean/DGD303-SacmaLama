using UnityEngine;

namespace Shmup
{
    public class MoveSin : MonoBehaviour
    {
        float sinCenterY;
        public float amplitude = 2;
        public float frequency = 2;
        public float speed = 2; // H�z de�i�keni

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

            // X pozisyonunu h�zla �arparak g�ncelle
            pos.x -= speed * Time.fixedDeltaTime;

            // Sin�s dalgas� hareketi
            float sin = Mathf.Sin(pos.x * frequency) * amplitude;
            pos.y = sinCenterY + sin;

            transform.position = pos;
        }
    }
}
