using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;

    public float shootIntervalSeconds = 0.5f; // �ki sald�r� aras� s�re
    public bool isActive = false;

    private float shootCooldown = 0f; // So�uma s�resi

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
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet gobullet = go.GetComponent<Bullet>();
        gobullet.direction = direction;
    }
}
