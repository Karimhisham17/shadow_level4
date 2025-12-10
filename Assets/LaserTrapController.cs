using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrapController : MonoBehaviour
{
    public int damage = 1;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                // تأكد إن اسم الحالة هنا هو نفس اسم الحالة في الأنيميتور عندك
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("active traps") || anim.GetCurrentAnimatorStateInfo(0).IsName("Active trap"))
                {
                    playerStats.TakeDamage(damage);
                }
            }
        }
    }
}