using UnityEngine;

public class Attack : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public Bullet bullet;
    private Vector2 direction;

    public float shootIntervalSeconds = 0.5f; // Ýki saldýrý arasý süre
    public bool isActive = false;

    private float shootTimer = 0f; // Saldýrý zamanlayýcý

    void Update()
    {
        if (!isActive)
            return;

        // Yönü ayarla
        direction = (transform.localRotation * Vector2.right).normalized;

        // Tuþ basýlý tutuluyorsa
        if (Input.GetKey(KeyCode.Space))
        {
            shootTimer += Time.deltaTime;

            // Zamanlayýcý yeterince büyükse saldýr
            if (shootTimer >= shootIntervalSeconds)
            {
                Shoot();
                shootTimer = 0f; // Zamanlayýcýyý sýfýrla
            }
        }
        else
        {
            // Tuþ býrakýldýðýnda zamanlayýcýyý sýfýrla
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
