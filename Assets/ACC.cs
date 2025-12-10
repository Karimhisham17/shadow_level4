using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACC : MonoBehaviour
{
    // متغير لتخزين مكون الأنيميتور
    private Animator anim;

    // اسم متغير الـ Bool في الأنيميتور الذي نريد تغييره
    // تأكد أن الاسم هنا يطابق الاسم في الـ Animator تماماً (حالة الأحرف مهمة)
    private string boolParamName = "ACC";

    void Start()
    {
        // الحصول على مكون الأنيميتور الموجود على نفس الكائن
        anim = GetComponent<Animator>();

        // التحقق من وجود الأنيميتور لتجنب الأخطاء
        if (anim == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    // هذه الدالة تعمل تلقائياً عندما يدخل اللاعب إلى منطقة الـ Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // التحقق مما إذا كان الكائن الذي دخل هو اللاعب
        if (other.CompareTag("Player"))
        {
            // إذا كان هناك أنيميتور، قم بتغيير قيمة متغير Bool إلى false
            if (anim != null)
            {
                // هنا التغيير الأساسي: نستخدم SetBool بدلاً من SetTrigger
                // ونمرر له الاسم والقيمة الجديدة (false)
                anim.SetBool(boolParamName, true);

                // اختياري: للتأكد من أن الكود يعمل
                // Debug.Log("ACC set to FALSE!");
            }
        }
    }

    // اختياري: إذا كنت تريد إعادة القيمة لـ true عند خروج اللاعب من المنطقة
    // قم بإزالة علامات التعليق (//) عن الدالة التالية:
    /*
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (anim != null)
            {
                anim.SetBool(boolParamName, true);
            }
        }
    }
    */
}