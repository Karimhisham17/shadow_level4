using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Video;

public class NPCnancy2 : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private Animator anim;
    public VideoPlayer videoPlayer;
    private bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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

            if (hasPlayed) return;

            if (other.CompareTag("Player"))
            {
                hasPlayed = true;
                videoPlayer.Play();
            }
            // تدمير الكولايدر عشان الحوار ميتعادش تاني
            Destroy(GetComponent<BoxCollider2D>());
        }
    }


}

