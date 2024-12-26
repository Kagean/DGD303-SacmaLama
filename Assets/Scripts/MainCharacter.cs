using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    Attack[] attacks;

    float moveSpeed = 3;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;

    int powerUpGunLevel = 0;
    GameObject shield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
        attacks = transform.GetComponentsInChildren<Attack>();
        foreach (Attack attack in attacks)
        {
            attack.isActive = true;
            if (attack.powerUpLevelRequirement != 0)
            {
                attack.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot)
        {
            shoot = false;
            foreach (Attack attack in attacks)
            {
                if (attack.gameObject.activeSelf)
                {
                    attack.Shoot();
                }

            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.deltaTime;
        if (speedUp)
        {
            moveAmount *= 3;
        }
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        Debug.Log(moveMagnitude);

        pos += move;
        if (pos.x <= 0.5f)
        {
            pos.x = 0.5f;
        }
        if (pos.x >= 17f)
        {
            pos.x = 17;
        }
        if (pos.y <= 1)
        {
            pos.y = 1;
        }
        if (pos.y >= 9)
        {
            pos.y = 9;
        }

        transform.position = pos;
    }

    void ActivadeShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }

    void AddAttacks()
    {
        powerUpGunLevel++;
        foreach(Attack attack in attacks)
        {
            if (attack.powerUpLevelRequirement == powerUpGunLevel)
            {
                attack.gameObject.SetActive(true);
            }
        }
    }

    void IncreaseSpeed()
    {
        moveSpeed*= 1.25f;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }

        Destroy destroy = collision.GetComponent<Destroy>();
        if (destroy != null)
        {
            if (HasShield())
            {
                DeactivateShield();
            }
            else
            {
                Destroy(gameObject);
            }
            Destroy(destroy.gameObject);
        }

        PowerUP powerUP = collision.GetComponent<PowerUP>();
        if (powerUP)
        {
            if (powerUP.activateShield)
            {
                ActivadeShield();
            }

            if (powerUP.addAttacks)
            {
                AddAttacks();
            }
            if (powerUP.increaseSpeed)
            {
                IncreaseSpeed();
            }
            Level.Instance.AddScore(powerUP.pointValue);
            Destroy(powerUP.gameObject);
        }
    }
}
