using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;

    [Header("Keys")]
    public KeyCode Spacebar = KeyCode.Space;
    public KeyCode L = KeyCode.LeftArrow;
    public KeyCode R = KeyCode.RightArrow;
    public KeyCode shootKey = KeyCode.F; // زر الإطلاق

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // اسحب الرصاصة هنا
    public Transform shootingPoint; // اسحب نقطة الإطلاق هنا

    // مرجع للـ SpriteRenderer لمعرفة الاتجاه
    public SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. القفز
        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            Jump();
        }

        // 2. الحركة لليسار
        if (Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (sr != null) sr.flipX = true; // اللاعب ينظر لليسار
        }

        // 3. الحركة لليمين
        else if (Input.GetKey(R)) // استخدمنا Else if لمنع الحركة في الاتجاهين معاً
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (sr != null) sr.flipX = false; // اللاعب ينظر لليمين
        }
        else
        {
            // (اختياري) وقف اللاعب لو شال إيده من الزراير
            // GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        // 4. إطلاق النار
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (groundCheck != null)
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    void Shoot()
    {
        // التأكد من وجود الرصاصة ونقطة الإطلاق لتجنب الأخطاء
        if (bulletPrefab != null && shootingPoint != null)
        {
            // إنشاء الرصاصة
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        }
        else
        {
            Debug.LogError("Shooting settings are missing! Check the Inspector.");
        }
    }
}