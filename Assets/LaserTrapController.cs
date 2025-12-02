using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrapController : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Optional: continuous damage while touching
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
    }
}
