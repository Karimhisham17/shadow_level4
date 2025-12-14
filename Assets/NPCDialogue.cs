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

    [Header("Audio Settings")]
    public AudioClip laughSound; // 1. ضع ملف صوت الضحك هنا في الـ Inspector
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // 2. تجهيز مشغل الصوت أوتوماتيكياً
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
            // (تم استبدال النص بنقاط للتوضيح)
            string[] dialogue = { "..." };

            // 3. تشغيل صوت الضحك هنا
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

        while (dialogueManager.dialogueBox.activeInHierarchy)
        {
            yield return null;
        }

        Debug.Log("Dialogue Finished! Walking away...");

        if (sr != null) sr.flipX = false;

        isWalking = true;

        Destroy(gameObject, 5f);
    }
}