using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float smoothness = 0.1f;
        bool shoot;
        Attack[] attacks;

        [SerializeField] GameObject model;

        [Header("Camera Bounds")]
        [SerializeField]
        Transform cameraFollow;

        [SerializeField] float minX = -8f;
        [SerializeField] float maxX = 8f;
        [SerializeField] float minY = -4f;
        [SerializeField] float maxY = 4f;

        InputReader input;

        Vector3 currentVelocity;
        Vector3 targetPosition;

        void Start()
        {
            input = GetComponent<InputReader>();
            targetPosition = transform.position;
            attacks = transform.GetComponentsInChildren<Attack>();
            foreach (Attack attack in attacks)
            {
                attack.isActive = true;
                if (attack.powerUpLevelRequirement != 0)
                {
                    attack.gameObject.SetActive(false);
                }
            }
        }

        void Update()
        {
            Vector3 cameraOffset = Vector3.right * cameraFollow.GetComponent<CameraController>().Speed * Time.deltaTime;

            targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime) + cameraOffset;

            var minPlayerX = cameraFollow.position.x + minX;
            var maxPlayerX = cameraFollow.position.x + maxX;
            var minPlayerY = cameraFollow.position.y + minY;
            var maxPlayerY = cameraFollow.position.y + maxY;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);

            shoot = Input.GetKeyDown(KeyCode.Space);
            if (shoot)
            {
                shoot = false;
                foreach (Attack attack in attacks)
                {
                    if (attack.gameObject.activeSelf)
                    {
                        attack.Shoot();
                    }

                }
            }

        }

    }
}