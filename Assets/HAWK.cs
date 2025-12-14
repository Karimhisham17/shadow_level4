using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAWK : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform player;
    public float moveSpeed = 3f;
    public float followDistance = 8f;

    [Header("Audio Settings")]
    public AudioClip walkSound; // Drag walk sound file here
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Automatically setup audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkSound;
        audioSource.loop = true; // Loop sound
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f; // Sound volume (changeable)

        // If player is missing in Inspector, find it automatically
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

        // If player is too far, stop movement
        if (distance > followDistance)
        {
            rb.velocity = Vector2.zero;

            // Stop sound if playing because movement stopped
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return;
        }

        // 1. Calculations: Determine direction towards player
        Vector2 direction = (player.position - transform.position).normalized;

        // 2. Movement: Apply velocity
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // --- Audio Playback Code ---
        // Check if X velocity is greater than 0.1 (moving)
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            // If sound isn't playing, play it
            if (!audioSource.isPlaying && walkSound != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If velocity is zero (stopped), stop sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        // -----------------------

        // 3. Direction (flip logic):
        // Since the sprite faces "Left" by default:
        if (rb.velocity.x > 0.1f)
        {
            // Enemy moving right, flip sprite to face right
            sr.flipX = true;
        }
        else if (rb.velocity.x < -0.1f)
        {
            // Enemy moving left, sprite is already left, keep as is
            sr.flipX = false;
        }
    }
}