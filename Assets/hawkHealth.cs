using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hawkHealth : MonoBehaviour
{
    public int health = 10;
    public GameObject deathEffect;
    public bool isInvulnerable = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        // تمت إزالة كود التحول لوضع الغضب (Enraged)

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}