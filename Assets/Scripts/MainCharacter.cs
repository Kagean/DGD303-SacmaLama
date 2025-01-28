using Unity.VisualScripting;
using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        public float health = 3;
        bool invincible = false;
        float invincibleTimer = 0;
        public float invincibleTime = 2;
        float blinkTimer = 0;
        public float blinkInterval = 0.3f;

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

        [SerializeField]
        GameOverScreen gameOverScreen; // GameOverScreen referansý

        InputReader input;

        Vector3 currentVelocity;
        Vector3 targetPosition;
        SpriteRenderer spriteRenderer;

        void Start()
        {
            input = GetComponent<InputReader>();
            targetPosition = transform.position;
            attacks = transform.GetComponentsInChildren<Attack>();
            spriteRenderer = model.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
            {
                spriteRenderer = model.AddComponent<SpriteRenderer>();
            }

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

            if (invincible)
            {
                if (invincibleTimer <= 0)
                {
                    invincible = false;
                    spriteRenderer.enabled = true; // Karakteri görünür yap
                }
                else
                {
                    invincibleTimer -= Time.deltaTime;
                    blinkTimer -= Time.deltaTime;
                    if (blinkTimer <= 0)
                    {
                        spriteRenderer.enabled = !spriteRenderer.enabled; // Görünürlüðü deðiþtir
                        blinkTimer = blinkInterval;
                    }
                }
            }

            if (health <= 0)
            {
                GameOver(); // Game Over fonksiyonunu çaðýr
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (invincible) return;

            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null && bullet.isEnemy)
            {
                TakeDamage(bullet.damage);
                Destroy(bullet.gameObject);
            }

            Meteor meteor = collision.GetComponent<Meteor>();
            if (meteor != null)
            {
                TakeDamage(1);
            }

            Destroy destroy = collision.GetComponent<Destroy>();
            if (destroy != null)
            {
                TakeDamage(1);
            }
        }

        void TakeDamage(int damage)
        {
            if (invincible) return;

            health -= damage;
            if (health <= 0)
            {
                GameOver();
            }
            else
            {
                invincible = true;
                invincibleTimer = invincibleTime;
                blinkTimer = blinkInterval;
            }
        }

        void GameOver()
        {
            // Kamera kontrolünü durdur
            CameraController cameraController = cameraFollow.GetComponent<CameraController>();
            if (cameraController != null)
            {
                cameraController.StopMoving(); // Kamera hareketini durdur
            }

            if (gameOverScreen != null)
            {
                gameOverScreen.Setup(); // Game Over ekranýný hazýrla
            }

            Destroy(gameObject); // Karakteri yok et
            Time.timeScale = 0f;
        }
    }
}
