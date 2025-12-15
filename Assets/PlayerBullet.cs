using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 2f;
    public int damage = 10; // قيمة الضرر (خليها رقم معقول، مثلا 10 عشان يبان في السلايدر)

    private Rigidbody2D rb;
    private PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();

        if (player != null && player.sr.flipX)
        {
            rb.velocity = new Vector2(-speed, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // إذا اصطدمت بالعدو (تأكد إن التاج بتاعه HAWK2 زي ما أنت كاتب)
        if (other.CompareTag("HAWK2"))
        {
            // 1. محاولة الوصول لسكربت الصحة اللي على العدو
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            // 2. لو السكربت موجود، اخصم من دمه
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 3. دمر الرصاصة فقط (ولا تدمر العدو هنا)
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}