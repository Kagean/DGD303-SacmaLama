using UnityEngine;

public class TeleportEnemy2D : MonoBehaviour
{
    public float teleportX = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 newPosition = other.transform.position;
            newPosition.x = teleportX;
            other.transform.position = newPosition;
        }
    }
}
