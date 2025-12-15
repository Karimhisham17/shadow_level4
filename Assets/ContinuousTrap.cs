using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousTrap : MonoBehaviour
{
    [Header("Boss Reference")]
    public GameObject hawkBoss; // 1. اسحب الزعيم HAWK هنا ضروري

    [Header("Settings")]
    public int damage = 1;

    [Header("Audio")]
    public AudioClip laserSound; // ملف الصوت
    private AudioSource audioSource;

    private Animator anim;
    private Collider2D trapCollider;
    private bool isBossDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        trapCollider = GetComponent<Collider2D>();

        // إعداد الصوت أوتوماتيك
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = laserSound;
        audioSource.loop = true; // تكرار مستمر
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 2f;
        audioSource.maxDistance = 10f;

        // تشغيل أولي (عشان يبدأ شغال علطول)
        if (audioSource.clip != null) audioSource.Play();
    }

    void Update()
    {
        // 1. فحص موت الزعيم
        if (hawkBoss == null)
        {
            if (!isBossDead)
            {
                TurnOff(); // افصل الفخ
            }
            return;
        }

        // 2. طالما الزعيم عايش: تأكد إن الفخ شغال (حماية إضافية)
        // لو لسبب ما الصوت وقف أو الكولايدر اتقفل، الكود ده هيشغلهم تاني
        if (!isBossDead)
        {
            if (trapCollider != null) trapCollider.enabled = true;
            if (audioSource.clip != null && !audioSource.isPlaying) audioSource.Play();

            // إجبار الأنيميشن يفضل على وضع التشغيل (اختياري، لو الاسم مطابق)
            if (anim != null) anim.Play("Active trap");
        }
    }
    void TurnOff()
    {
        isBossDead = true;

        // 1. وقف الصوت
        if (audioSource.isPlaying) audioSource.Stop();

        // 2. الغي الضرر
        if (trapCollider != null) trapCollider.enabled = false;

        // 3. (التعديل المهم) إخفاء الكائن بالكامل بدل تجميد الأنيميشن
        // شيل السطر القديم بتاع anim.enabled = false
        // واستخدم ده:
        gameObject.SetActive(false);

        // أو لو عايز القاعدة تفضل موجودة والليزر بس يختفي استخدم السطر ده بداله:
        // GetComponent<SpriteRenderer>().enabled = false;

        Debug.Log("Boss Died -> Continuous Trap OFF");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // لو الزعيم مات، مفيش دمج
        if (isBossDead || hawkBoss == null) return;

        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // اضرب علطول بدون انتظار أنيميشن
                playerStats.TakeDamage(damage);
            }
        }
    }
}