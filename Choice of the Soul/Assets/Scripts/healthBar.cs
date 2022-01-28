using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image bar;

    public void TakeDamageFill(float damage)
    {
        bar.fillAmount -= damage * 0.1f;
    }

    public void TakeHealthFill(float health)
    {
        movePlayer.Instance.health += health;
        if (movePlayer.Instance.health > 10f) movePlayer.Instance.health = 10f;
        bar.fillAmount += health * 0.1f;
    }
}
