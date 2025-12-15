using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCnancy : MonoBehaviour
{
    private Animator anim;
    private bool isPlayerInRange = false; // متغير لمعرفة هل اللاعب قريب أم لا

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // الشرط: لو اللاعب موجود في المنطقة + ضغط على حرف H
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.H))
        {
            // تشغيل الأنيميشن
            if (anim != null)
            {
                anim.SetBool("Hacker", true);
            }

            // تدمير الكولايدر عشان الأنيميشن ميتعادش
            Destroy(GetComponent<BoxCollider2D>());

            // (اختياري) نلغي تفعيل السكربت لتوفير الأداء
            this.enabled = false;
        }
    }

    // لما اللاعب يدخل المنطقة
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // اللاعب وصل
        }
    }

    // لما اللاعب يخرج من المنطقة (عشان لو داس H وهو بعيد ميحصلش حاجة)
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // اللاعب مشي
        }
    }
}