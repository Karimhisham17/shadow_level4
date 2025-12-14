using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 0.5f; // خليتها نص ثانية عشان يضرب بسرعة

    [Header("Audio Settings")]
    public AudioClip shootSound;
    private AudioSource audioSource;

    private float timer;
    private bool canShoot = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        timer = timeBetweenShots; // عشان يبقى جاهز يضرب أول ما يشوفك
    }

    void Update()
    {
        if (canShoot)
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenShots)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        if (firePoint != null && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (shootSound != null) audioSource.PlayOneShot(shootSound);

            Debug.Log("🔥 POLAT!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canShoot = true;
            // السطر ده بيخليه يضرب طلقة فوراً أول ما تدخل
            if (timer >= timeBetweenShots) Shoot();
            timer = 0;
            Debug.Log("✅ ATTSCK ");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canShoot = false;
            Debug.Log("❌ DONT ATTACK");
        }
    }
}