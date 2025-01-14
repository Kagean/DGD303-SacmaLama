using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int currentCount = 0;

    public float initialForceX = 5000f;
    public float initialForceY = -450f;
    public float speedThreshold = 2f;
    public float randomSpeedRange = 3f;

    void Go()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(initialForceX, initialForceY));
        }
        else
        {
            rb2d.AddForce(new Vector2(-initialForceX, initialForceY));
        }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.linearVelocity = new Vector2(5f, -5f);

        Invoke("Go", 0.1f);
    }

    void Update()
    {
        if (Mathf.Abs(rb2d.linearVelocity.x) < speedThreshold)
        {
            rb2d.linearVelocity = new Vector2(Random.Range(3, randomSpeedRange), rb2d.linearVelocity.y);
        }
        if (Mathf.Abs(rb2d.linearVelocity.y) < speedThreshold)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, Random.Range(3, randomSpeedRange));
        }
    }
}
