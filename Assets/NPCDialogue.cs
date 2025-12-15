using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    private bool isWalking = false;
    private SpriteRenderer sr;
    private Animator anim; // 1. متغير للأنيميشن

    [Header("Audio Settings")]
    public AudioClip laughSound;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // 2. تعريف الأنيميتور

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isWalking)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            string[] dialogue = { "..." }; // ضع حوارك هنا

            if (laughSound != null)
            {
                audioSource.PlayOneShot(laughSound);
            }

            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

            Destroy(GetComponent<BoxCollider2D>());

            StartCoroutine(WaitAndWalk());
        }
    }

    IEnumerator WaitAndWalk()
    {
        yield return new WaitForSeconds(0.5f);

        // انتظار انتهاء الحوار
        while (dialogueManager.dialogueBox.activeInHierarchy)
        {
            yield return null;
        }

        Debug.Log("Dialogue Finished! Walking away...");

        // ضبط اتجاه الوجه
        if (sr != null) sr.flipX = false;

        // 3. تشغيل أنيميشن المشي
        if (anim != null)
        {
            anim.SetBool("move", true);
        }

        isWalking = true;

        Destroy(gameObject, 5f);
    }
}