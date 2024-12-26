using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool useReset = true;
    public float startX = 3f;
    public float resetThreshold = -3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (useReset && pos.x < resetThreshold)
        {
            pos.x = startX;
        }


        transform.position = pos;
    }
}
