using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public int damage = 1;
    public float lifeTime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // الحركة (يمين أو يسار حسب ما تحط السالب)
        rb.velocity = -transform.right * speed;

        // الرصاصة كدة كدة هتختفي لوحدها بعد 3 ثواني
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // الشرط الوحيد: لو خبطت في اللاعب بس
        if (hitInfo.CompareTag("Player"))
        {
            PlayerStats player = hitInfo.GetComponent<PlayerStats>();
            // أو استخدم GetComponentInParent لو السكربت على الأب
            // PlayerStats player = hitInfo.GetComponentInParent<PlayerStats>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // دمر الرصاصة لما تضرب اللاعب
            Destroy(gameObject);
        }

        // مسحنا الجزء بتاع Ground و Wall خلاص 🗑️
    }


}