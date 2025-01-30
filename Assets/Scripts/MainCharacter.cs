using Unity.VisualScripting;
using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        public float health = 3;
        private const float maxHealth = 3;
        bool invincible = false;
        float invincibleTimer = 0;
        public float invincibleTime = 2;
        float blinkTimer = 0;
        public float blinkInterval = 0.3f;

        [SerializeField] float baseSpeed = 5f; // Temel hýz
        [SerializeField] float speed = 5f;
        [SerializeField] float smoothness = 0.1f;
        bool shoot;
        Attack[] attacks;

        [SerializeField] GameObject model;

        GameObject Shield;
        int powerupAttackLevel = 0;

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

        private Level level; // Level referansý

        private float attackSpeedMultiplier = 1f; // Saldýrý hýzý çarpaný

        void Start()
        {
            Shield = transform.Find("Shield").gameObject;
            DeactivateShield();
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

            level = Level.Instance; // Level referansýný al
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

            // Hýzý 8f ile sýnýrla
            speed = Mathf.Min(speed, 8f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null && bullet.isEnemy)
            {
                if (!invincible)
                {
                    TakeDamage(bullet.damage);
                }
                Destroy(bullet.gameObject);
            }

            Meteor meteor = collision.GetComponent<Meteor>();
            if (meteor != null)
            {
                if (!invincible)
                {
                    TakeDamage(1);
                }
            }

            Destroy destroy = collision.GetComponent<Destroy>();
            if (destroy != null)
            {
                if (!invincible)
                {
                    TakeDamage(1);
                }
            }

            PowerUp powerUp = collision.GetComponent<PowerUp>();
            if (powerUp)
            {
                if (powerUp.activateShield)
                {
                    ActivadeShield();
                }
                if (powerUp.activateDoubleShot)
                {
                    addAttacks();
                }
                if (powerUp.increaseSpeed)
                {
                    speed = Mathf.Min(speed + 1, 8f); // Hýzý artýr ve 8f ile sýnýrla
                }
                if (powerUp.increaseHealth && health < 3) // Can azalmýþsa can ekle
                {
                    health += 1;
                }
                if (powerUp.increaseAttackSpeed)
                {
                    IncreaseAttackSpeed(0.1f); // Saldýrý hýzýný artýr
                }
                if (powerUp.points)
                {
                    level.AddScore(powerUp.pointValue); // Puaný Level sýnýfýna ekle
                }
                if (powerUp.Joker)
                {
                    // Joker
                }
                Destroy(powerUp.gameObject);
            }
        }

        void TakeDamage(int damage)
        {
            if (invincible) return;

            if (hasShield())
            {
                DeactivateShield();
                invincible = true;
                invincibleTimer = invincibleTime;
                blinkTimer = blinkInterval;
                return;
            }

            health -= damage;
            speed = Mathf.Max(baseSpeed, speed - 2); // Hýzý azalt ve temel hýzýn altýna düþmediðinden emin ol
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

        void ActivadeShield()
        {
            Shield.SetActive(true);
        }

        void DeactivateShield()
        {
            Shield.SetActive(false);
        }

        bool hasShield()
        {
            return Shield.activeSelf;
        }

        void addAttacks()
        {
            powerupAttackLevel++;
            foreach (Attack attack in attacks)
            {
                if (attack.powerUpLevelRequirement == powerupAttackLevel)
                {
                    attack.gameObject.SetActive(true);
                }
            }
        }

        public void IncreaseHealth(float amount)
        {
            health = Mathf.Min(health + amount, maxHealth); // Caný artýr ve maksimum deðeri aþma
        }

        public void IncreaseAttackSpeed(float amount)
        {
            attackSpeedMultiplier += amount;
            foreach (Attack attack in attacks)
            {
                attack.UpdateAttackSpeed(attackSpeedMultiplier);
            }
        }
    }
}
