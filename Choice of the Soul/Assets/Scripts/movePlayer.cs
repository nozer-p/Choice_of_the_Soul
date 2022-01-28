using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float speedRun;
    public float jumpForce;
    public float health;
    public GameObject deathEffect;
    private float moveInput;
    private Rigidbody2D rb;
    private healthBar hb;

    public void Recoil()
    {
        float sideRecoil = 0f;
        if (!facingRight)
        {
            sideRecoil = 1f;
        }
        else sideRecoil = -1f;
        //rb.velocity = Vector2.up * 7;
        rb.transform.Translate(new Vector2(sideRecoil * 0.06f, 0.02f));
    }

    public static movePlayer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private bool isGrounded;
    public Transform feetPos;
    public Transform shotPoint;
    public float checkRad;
    public LayerMask whatIsGround;

    private void Start()
    {
        hb = FindObjectOfType<healthBar>();
        rb = GetComponent<Rigidbody2D>();
    }

    private float y1;
    private float y2;
    private bool fall = false;
    private bool takeOff = false;
    private bool facingRight = true;

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        shotPoint.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRad, whatIsGround);
        if (isGrounded == true)
        {
            if (!fall)
            {
                y1 = y2 = transform.position.y;
                shakeCamera.Instance.Shake(false);
            }
            else
            {
                y2 = transform.position.y;
                if (Mathf.Abs(y1 - y2) >= 10)
                {
                    shakeCamera.Instance.Shake(true);
                }
                fall = false;
            }
        }
        else
        {
            if (!fall)
            {
                y1 = transform.position.y;
                fall = true;
            }
            if (takeOff)
            {
                if (y1 < transform.position.y)
                {
                    y1 = transform.position.y;
                }
            }
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        //if (Input.GetKeyDown(KeyCode.W))
        {
            takeOff = true;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (health <= 0f)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speedRun, rb.velocity.y);
        if (!facingRight && moveInput > 0f)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0f)
        {
            Flip();
        }
    }

    public void TakeDamage(float damage)
    {
        hb.TakeDamageFill(damage);
        health -= damage;
    }
}
