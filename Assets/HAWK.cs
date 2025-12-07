using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAWK : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float followDistance = 8f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // لو نسيت تحط اللاعب في الانسبيكتور، الكود هيلاقيه لوحده
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // لو اللاعب بعيد أوي، وقف الحركة
        if (distance > followDistance)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // 1. الحسابات: حدد الاتجاه نحو اللاعب
        Vector2 direction = (player.position - transform.position).normalized;

        // 2. الحركة: طبق السرعة
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // 3. الاتجاه (التصليح هنا):
        // بما إن الرسمة بتبص "شمال" (Left):

        if (rb.velocity.x > 0.1f)
        {
            // العدو ماشي يمين، لازم نعكس الرسمة عشان يبص يمين
            sr.flipX = true;
        }
        else if (rb.velocity.x < -0.1f)
        {
            // العدو ماشي شمال، الرسمة أصلاً شمال، سيبها زي ما هي
            sr.flipX = false;
        }
    }
}