using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [Header("إعدادات الهجوم")]
    public int attackDamage = 1;      // كمية الضرر
    public float attackRange = 1.5f;  // حجم دائرة الضربة (الحمراء)
    public Vector3 attackOffset;      // مكان الضربة بالنسبة للزعيم
    public LayerMask attackMask;      // لازم تختار Player Layer هنا

    [Header("إعدادات الكشف")]
    public float detectionRange = 4f; // مسافة الرؤية (الصفراء)

    private SpriteRenderer sr;
    private Transform playerTransform;
    private Animator anim;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // البحث عن اللاعب الحقيقي
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null || anim == null) return;

        // 1. حساب المسافة الحقيقية
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        // 2. لو المسافة تسمح + الزعيم مش بيضرب دلوقتي حالياً
        if (distance <= detectionRange)
        {
            // نتأكد إن الأنيميشن مش شغال حالياً عشان ميعلقش
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetTrigger("Attack");
            }
        }
    }

    // ---------------------------------------------------------
    // هذه الدالة السحرية: لازم يتم استدعاؤها من الـ Animation Event
    // ---------------------------------------------------------
    public void ApplyDamage()
    {
        Vector3 pos = transform.position;

        // تحديد اتجاه الضربة (يمين ولا شمال)
        float multiplier = (sr != null && sr.flipX) ? 1f : -1f;
        pos += transform.right * attackOffset.x * multiplier;
        pos += transform.up * attackOffset.y;

        // كشف التصادم
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);

        foreach (Collider2D col in hitObjects)
        {
            if (col.CompareTag("Player"))
            {
                PlayerStats playerStats = col.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(attackDamage);
                    Debug.Log("تم خصم صحة من اللاعب! (Damage Applied)");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        Vector3 pos = transform.position;
        float multiplier = (sr != null && sr.flipX) ? 1f : -1f;
        pos += transform.right * attackOffset.x * multiplier;
        pos += transform.up * attackOffset.y;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}