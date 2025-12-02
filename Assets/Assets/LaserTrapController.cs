using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrapController : MonoBehaviour
{
    public int damage = 1;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerStats != null)
        {
            playerStats.TakeDamage(damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerStats != null)
        {
            playerStats.TakeDamage(damage);
        }
    }
}
