using UnityEngine;

namespace Shmup
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] float speed = 2f;

        void Start() => transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        private void LateUpdate()
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
