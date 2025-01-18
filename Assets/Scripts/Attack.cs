using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;

    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f; // Ýki saldýrý arasý süre
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0.0f;
    float delayTimer = 0.0f;
    public bool isActive = false;

    private float shootCooldown = 0f; // Soðuma süresi

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
                if (shootTimer >= shootIntervalSeconds)
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
}
