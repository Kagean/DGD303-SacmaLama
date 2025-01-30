using UnityEngine;

namespace Shmup
{
    public class TargetLock : MonoBehaviour
    {
        public Transform player;
        public float speed = 5f;

        void Start()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player").transform;
            }
        }


        void Update()
        {
            if (player != null)
            {

                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
