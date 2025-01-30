using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f; // �ki sald�r� aras�ndaki s�re
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0.0f;
    float delayTimer = 0.0f;
    public bool isActive = false;
    private float shootCooldown = 0f; // So�uma s�resi

    private float originalShootIntervalSeconds; // Orijinal sald�r� s�resi

    void Start()
    {
        originalShootIntervalSeconds = shootIntervalSeconds; // Orijinal sald�r� s�resini sakla
    }

    void Update()
    {
        if (!isActive)
            return;

        // Y�n� ayarla
        direction = (transform.localRotation * Vector2.right).normalized;

        // Tu� bas�l� tutuluyorsa
        if (Input.GetKey(KeyCode.Space))
        {
            // E�er belirlenen so�uma s�resi ge�mi�se, ate� et
            shootCooldown += Time.deltaTime;
            if (shootCooldown >= shootIntervalSeconds)
            {
                Shoot();
                shootCooldown = 0f; // So�uma s�resini s�f�rla
            }
        }
        else
        {
            shootCooldown = 0f; // Tu� b�rak�ld���nda so�uma s�resini s�f�rla
        }

        if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= originalShootIntervalSeconds) // Orijinal sald�r� s�resini kullan
                {
                    Shoot();
                    shootTimer = 0f;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet gobullet = go.GetComponent<Bullet>();
        gobullet.direction = direction;
    }

    public void UpdateAttackSpeed(float multiplier)
    {
        shootIntervalSeconds /= multiplier; // Sadece manuel sald�r�lar i�in sald�r� s�resini g�ncelle
    }
}
