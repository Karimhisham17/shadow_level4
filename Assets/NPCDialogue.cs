using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Movement Settings")]
    public float moveSpeed = 3f; // سرعة المشي
    private bool isWalking = false; // متغير عشان نعرف هو بيمشي ولا لأ
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // لو الحوار خلص والمتغير ده بقى true، الشخصية تمشي
        if (isWalking)
        {
            // الحركة ناحية اليمين (ممكن تغير Vector2.right لـ Vector2.left لو عايزه يمشي شمال)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            string[] dialogue = { "Nigger fuck off you fucking black "
                                  };

            // إرسال الجمل وبدء الحوار
            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

            // مسح الكولايدر عشان الحوار ميتعادش
            Destroy(GetComponent<BoxCollider2D>());

            // تشغيل الكوروتين اللي هيستنى الحوار يخلص
            StartCoroutine(WaitAndWalk());
        }
    }

    // كوروتين بيراقب الحوار
    IEnumerator WaitAndWalk()
    {
        // استنى ثانية صغيرة عشان نلحق نفتح صندوق الحوار
        yield return new WaitForSeconds(0.5f);

        // "افضل مستني طالما صندوق الحوار مفتوح"
        while (dialogueManager.dialogueBox.activeInHierarchy)
        {
            yield return null; // انتظر للفريم اللي بعده
        }

        // أول ما الكود يوصل هنا معناه الحوار اتقفل
        Debug.Log("Dialogue Finished! Walking away...");

        // (اختياري) نقلب وش الشخصية عشان تمشي في الاتجاه الصح
        if (sr != null) sr.flipX = false; // جرب تغيرها true لو مشيت بضهرها

        // تفعيل الحركة
        isWalking = true;

        // (اختياري) تدمير الشخصية بعد 5 ثواني من المشي عشان متفضلش ماشية للمالا نهاية
        Destroy(gameObject, 5f);
    }
}