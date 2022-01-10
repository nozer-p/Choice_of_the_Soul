using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float speedRun;
    public float jumpForce;

    private float moveInput;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRad;
    public LayerMask whatIsGround;

    private void Start()
    {
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
                if (Mathf.Abs(y1 - y2) >= 5)
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
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            takeOff = true;
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speedRun, rb.velocity.y);
        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
    }
}
