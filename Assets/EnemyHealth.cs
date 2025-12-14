using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI References")]
    public Slider healthSlider;
    public GameObject canvasObject; // اسحب الـ Canvas الخاص بالعدو هنا

    [Header("Visibility Settings")]
    public float showDistance = 5f; // المسافة التي يظهر عندها الشريط
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // إخفاء الشريط في البداية
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }

        // البحث عن اللاعب
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        // لو اللاعب مش موجود أو العدو مات، وقف الكود
        if (player == null || canvasObject == null) return;

        // حساب المسافة بين العدو واللاعب
        float distance = Vector2.Distance(transform.position, player.position);

        // لو المسافة قريبة (أقل من الرقم المحدد)
        if (distance <= showDistance)
        {
            canvasObject.SetActive(true); // أظهر الشريط
        }
        else
        {
            canvasObject.SetActive(false); // أخفِ الشريط
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died!");

        // تدمير العدو بالكامل (بما فيه الشريط لأنه ابن له)
        Destroy(gameObject);
    }
}