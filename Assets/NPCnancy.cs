using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCnancy : MonoBehaviour
{
    // متغير لربط مدير الحوار (لا تنسى تسحبه في الـ Inspector)
    public DialogueManager dialogueManager;
    private Animator anim;


    void Start()
    {

        anim = GetComponent<Animator>();
    }
    // دالة التصادم (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // التأكد أن الذي لمس الشخصية هو اللاعب
        if (other.CompareTag("Player"))
        {
            // مصفوفة الجمل (الحوار) كما هو موجود في التمرين
            string[] dialogue = {
                "Red Riding Hood: Um, hello? What exactly are you?",
                
            };

            // إرسال الجمل لمدير الحوار
            if (dialogueManager != null)
            {
                dialogueManager.SetSentences(dialogue);
                dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());
            }
            if (anim != null)
            {
                anim.SetBool("Hacker", true);
            }

            // تدمير الكولايدر عشان الحوار ميتعادش تاني
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}