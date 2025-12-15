    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nancytext : MonoBehaviour
{
    // اسحب هنا السكربت المسؤول عن عرض الكلام (Nancy)
    public NancyNpc nancy;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. التأكد أن اللاعب هو اللي قرب
        if (other.CompareTag("Player"))
        {
            // 2. كتابة الجمل هنا
            string[] dialogue = {
                "Red Riding Hood: Um, hello? What exactly are you?"
            };

            // 3. إرسال الجمل وتشغيل الحوار
            if (nancy != null)
            {
                nancy.SetSentences(dialogue);
                nancy.StartCoroutine(nancy.TypeDialogue());
            }

            // 4. مسح الكولايدر عشان الحوار ميتعادش
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
