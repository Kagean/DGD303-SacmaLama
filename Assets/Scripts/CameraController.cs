using UnityEngine;

namespace Shmup
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] float speed = 2f;

        public float Speed => speed;

        void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        private void LateUpdate()
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
