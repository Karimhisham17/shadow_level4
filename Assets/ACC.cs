using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACC : MonoBehaviour
{
    private Animator anim;

    // تأكد أن الاسم هنا يطابق اسم المتغير في نافذة Animator بالظبط
    private string boolParamName = "ACC";

    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    // هذه الدالة تعمل عند لمس اللاعب للكائن (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // نتأكد أن الذي لمس الكائن هو اللاعب
        if (other.CompareTag("Player"))
        {
            if (anim != null)
            {
                // نجعل القيمة true لتشغيل الأنيميشن
                anim.SetBool(boolParamName, true);
                // Debug.Log("Activated!"); 
            }
        }
    }

    // (اختياري) لو عايز الأنيميشن يقف لما اللاعب يبعد
    /*
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (anim != null)
            {
                anim.SetBool(boolParamName, false);
            }
        }
    }
    */
}