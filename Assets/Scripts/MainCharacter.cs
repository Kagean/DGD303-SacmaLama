using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Vector2 initialPosition;

    Attack[] attacks;

    float moveSpeed = 3;
    float speedMultiplier = 1;

    int hits = 3;
    bool invincible = false;
    float invincibleTimer = 0;
    float invincibleTime = 2;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;

    SpriteRenderer spriteRenderer;

    GameObject shield;
    int powerUpGunLevel = 0;

    private void Awake()
    {
        initialPosition = transform.position;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

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

        if (invincible)
        {

            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * speedMultiplier * Time.deltaTime;
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
            if (attack.powerUpLevelRequirement <= powerUpGunLevel)
            {
                attack.gameObject.SetActive(true);
            }
            else
            {
                attack.gameObject.SetActive(false);
            }
        }
    }

    void SetSpeedMultiplier(float mult)
    {
        speedMultiplier = mult;
    }

    void ResetMain()
    {
        transform.position = initialPosition;
        DeactivateShield();
        powerUpGunLevel = -1;
        AddAttacks();
        SetSpeedMultiplier(1);
        hits = 3;
        Level.Instance.ResetLevel();
    }

    void Hit(GameObject gameObjectHit)
    {
        if (HasShield())
        {
            DeactivateShield();
        }
        else
        {
            if (!invincible)
            {
                hits--;
                if (hits == 0)
                {
                    ResetMain();
                }
                else
                {
                    invincible = true;
                }
                Destroy(gameObjectHit);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Hit(bullet.gameObject);
            }
        }

        Destroy destroy = collision.GetComponent<Destroy>();
        if (destroy != null)
        {
            Hit(destroy.gameObject);
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
                SetSpeedMultiplier(speedMultiplier +1);
            }
            Level.Instance.AddScore(powerUP.pointValue);
            Destroy(powerUP.gameObject);
        }
    }
}
