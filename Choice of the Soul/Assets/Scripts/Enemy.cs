using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public float health;
    public float speed;
    public float damage;
    public GameObject deathEffect;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private scoreManager number;
    private healthBar hb;

    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;

    private void Start()
    {
        hb = FindObjectOfType<healthBar>();
        number = FindObjectOfType<scoreManager>();
        normalSpeed = speed;
    }

    private void Update()
    {
        if (timeBtwAttack <= 0f)
        {
           Collider2D[] player = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
           for (int i = 0; i < player.Length; i++)
           {
                player[i].GetComponent<movePlayer>().TakeDamage(damage);
           }
           timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (stopTime <= 0f)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0f;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0f)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            number.Kill();
            float chance = Random.Range(0f, 100f);
            if (chance >= 80f) hb.TakeHealthFill(Random.Range(0f, 2f));
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    /*public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                OnEnemyAttack();
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }*/

    /*public void OnEnemyAttack()
    {
        timeBtwAttack = startTimeBtwAttack;
        player.health -= damage;
    }*/

    public void TakeDamage(float damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
