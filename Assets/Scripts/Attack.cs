using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.3f; // Ýki saldýrý arasýndaki süre
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0.0f;
    float delayTimer = 0.0f;
    public bool isActive = false;
    private float shootCooldown = 0f; // Soðuma süresi

    private float originalShootIntervalSeconds; // Orijinal saldýrý süresi

    public bool missileAttackEnabled = false; // Misilleme saldýrýsýnýn aktif olup olmadýðýný kontrol eder
    public Transform playerTransform; // Oyuncunun transformu
    public float missileShootIntervalSeconds = 1.0f; // Misilleme saldýrýlarý arasýndaki süre
    private float missileShootTimer = 0.0f; // Misilleme saldýrýsý zamanlayýcýsý

    void Start()
    {
        originalShootIntervalSeconds = shootIntervalSeconds; // Orijinal saldýrý süresini sakla
    }

    void Update()
    {
        if (!isActive)
            return;

        // Yönü ayarla
        direction = (transform.localRotation * Vector2.right).normalized;

        // Tuþ basýlý tutuluyorsa
        if (Input.GetKey(KeyCode.Space))
        {
            // Eðer belirlenen soðuma süresi geçmiþse, ateþ et
            shootCooldown += Time.deltaTime;
            if (shootCooldown >= shootIntervalSeconds)
            {
                Shoot();
                shootCooldown = 0f; // Soðuma süresini sýfýrla
            }
        }
        else
        {
            shootCooldown = 0f; // Tuþ býrakýldýðýnda soðuma süresini sýfýrla
        }

        if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= originalShootIntervalSeconds) // Orijinal saldýrý süresini kullan
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

        // Misilleme saldýrýsý aktifse ve belirlenen süre geçmiþse oyuncuya doðru ateþ et
        if (missileAttackEnabled && playerTransform != null)
        {
            missileShootTimer += Time.deltaTime;
            if (missileShootTimer >= missileShootIntervalSeconds)
            {
                MissileAttack(playerTransform);
                missileShootTimer = 0.0f; // Zamanlayýcýyý sýfýrla
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
        shootIntervalSeconds /= multiplier; // Sadece manuel saldýrýlar için saldýrý süresini güncelle
        if (shootIntervalSeconds < 0.1f)
        {
            shootIntervalSeconds = 0.1f; // Saldýrý süresini minimum 0.1f olarak ayarla
        }
    }

    public void MissileAttack(Transform targetTransform)
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet gobullet = go.GetComponent<Bullet>();
        gobullet.isEnemy = true; // Merminin düþman mermisi olduðunu belirt
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
