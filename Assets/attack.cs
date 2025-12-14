using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [Header("Attack and Damage Settings")]
    public int attackDamage = 1;
    public float damageRange = 1.5f;
    public Vector3 attackOffset;
    public LayerMask attackMask;

    [Header("AI Distances")]
    public float soundRange = 15f;        // Aggro sound range (Scream)
    public float attackTriggerRange = 3f; // Attack start range

    [Header("Audio")]
    public AudioClip aggroClip;  // 1. Sound when spotting player
    public AudioClip attackClip; // 2. Attack sound
    // (walkClip removed from here)

    // Audio Sources
    private AudioSource fxSource;   // For effects (attack and aggro)

    private bool hasPlayedAggro = false;
    private SpriteRenderer sr;
    private Transform playerTransform;
    private Animator anim;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Setup audio source (for attack and aggro only)
        fxSource = gameObject.AddComponent<AudioSource>();
        fxSource.playOnAwake = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform == null || anim == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        // 1. Aggro Sound (Plays only once when player gets close)
        if (distance <= soundRange && !hasPlayedAggro)
        {
            if (aggroClip != null) fxSource.PlayOneShot(aggroClip);
            hasPlayedAggro = true;
        }

        // (Walk sound logic completely removed from here)

        // 2. Attack Logic
        if (distance <= attackTriggerRange)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetTrigger("Attack");
                // Note: Attack sound plays from ApplyDamage function below
            }
        }
    }

    // ---------------------------------------------------------
    // Animation Event Function
    // ---------------------------------------------------------
    public void ApplyDamage()
    {
        // 1. Play attack sound here
        
            fxSource.PlayOneShot(attackClip);


        // 2. Calculate Damage
        Vector3 pos = transform.position;
        float multiplier = (sr != null && sr.flipX) ? 1f : -1f;
        pos += transform.right * attackOffset.x * multiplier;
        pos += transform.up * attackOffset.y;

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(pos, damageRange, attackMask);
        foreach (Collider2D col in hitObjects)
        {
            if (col.CompareTag("Player"))
            {
                PlayerStats playerStats = col.GetComponent<PlayerStats>();
                if (playerStats != null) playerStats.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, damageRange); // Damage Range
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, attackTriggerRange); // Attack Start Range
        Gizmos.color = Color.blue; Gizmos.DrawWireSphere(transform.position, soundRange); // Vision/Aggro Range
    }
}