using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed;
    public int damage;
    public GameObject deathEffect;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    //private movePlayer player;

    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;

    private void Start()
    {
        //player = FindObjectOfType<movePlayer>();
        normalSpeed = speed;
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
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

        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
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

    public void TakeDamage(int damage)
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
