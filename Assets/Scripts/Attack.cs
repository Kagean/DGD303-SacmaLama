using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.3f; // �ki sald�r� aras�ndaki s�re
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0.0f;
    float delayTimer = 0.0f;
    public bool isActive = false;
    private float shootCooldown = 0f; // So�uma s�resi

    private float originalShootIntervalSeconds; // Orijinal sald�r� s�resi

    public bool missileAttackEnabled = false; // Misilleme sald�r�s�n�n aktif olup olmad���n� kontrol eder
    public Transform playerTransform; // Oyuncunun transformu
    public float missileShootIntervalSeconds = 1.0f; // Misilleme sald�r�lar� aras�ndaki s�re
    private float missileShootTimer = 0.0f; // Misilleme sald�r�s� zamanlay�c�s�

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

        // Misilleme sald�r�s� aktifse ve belirlenen s�re ge�mi�se oyuncuya do�ru ate� et
        if (missileAttackEnabled && playerTransform != null)
        {
            missileShootTimer += Time.deltaTime;
            if (missileShootTimer >= missileShootIntervalSeconds)
            {
                MissileAttack(playerTransform);
                missileShootTimer = 0.0f; // Zamanlay�c�y� s�f�rla
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
        if (shootIntervalSeconds < 0.1f)
        {
            shootIntervalSeconds = 0.1f; // Sald�r� s�resini minimum 0.1f olarak ayarla
        }
    }

    public void MissileAttack(Transform targetTransform)
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet gobullet = go.GetComponent<Bullet>();
        gobullet.isEnemy = true; // Merminin d��man mermisi oldu�unu belirt
        StartCoroutine(UpdateMissileDirection(gobullet, targetTransform));
    }

    private IEnumerator UpdateMissileDirection(Bullet missile, Transform targetTransform)
    {
        while (missile != null && targetTransform != null)
        {
            missile.direction = (targetTransform.position - missile.transform.position).normalized;
            yield return null; // Bir sonraki kareye kadar bekle
        }
    }

    public void ToggleMissileAttack(bool isEnabled)
    {
        missileAttackEnabled = isEnabled;
    }
}
