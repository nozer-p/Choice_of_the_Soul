using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;
    //private bool wall;
    public float attackRange;
    public float damage;

    private void Update()
    {
        //wall = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsGround);
        //if (!wall && timeBtwAttack <= 0)
        if (timeBtwAttack <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space) && switchWeapon.Instance.weaponSwitch == 0)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
