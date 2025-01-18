using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;

    public float shootIntervalSeconds = 0.5f; // �ki sald�r� aras� s�re
    public bool isActive = false;

    private float shootTimer = 0f; // Sald�r� zamanlay�c�

    void Update()
    {
        if (!isActive)
            return;

        // Y�n� ayarla
        direction = (transform.localRotation * Vector2.right).normalized;

        // Tu� bas�l� tutuluyorsa
        if (Input.GetKey(KeyCode.Space))
        {
            shootTimer += Time.deltaTime;

            // Zamanlay�c� yeterince b�y�kse sald�r
            if (shootTimer >= shootIntervalSeconds)
            {
                Shoot();
                shootTimer = 0f; // Zamanlay�c�y� s�f�rla
            }
        }
        else
        {
            // Tu� b�rak�ld���nda zamanlay�c�y� s�f�rla
            shootTimer = shootIntervalSeconds;
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet gobullet = go.GetComponent<Bullet>();
        gobullet.direction = direction;
    }
}
