using UnityEngine;

namespace Shmup
{
    public class MoveSin : MonoBehaviour
    {
        float sinCenterY;
        public float amplitude = 2;
        public float frequency = 2;
        public float speed = 2;

        void Start()
        {
            sinCenterY = transform.position.y;
        }

        void Update()
        {

        }
        private void FixedUpdate()
        {
            Vector2 pos = transform.position;

            pos.x -= speed * Time.fixedDeltaTime;

            float sin = Mathf.Sin(pos.x * frequency) * amplitude;
            pos.y = sinCenterY + sin;

            transform.position = pos;
        }
    }
}
