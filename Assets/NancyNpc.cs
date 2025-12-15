using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NancyNpc : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI textDisplay; // اسحب نص الكلام هنا
    public GameObject continueButton;   // اسحب زر Continue هنا
    public GameObject dialogueBox;      // اسحب خلفية الحوار (Panel) هنا

    [Header("Settings")]
    public float typingSpeed = 0.02f;   // سرعة الكتابة

    [Header("Player Ref")]
    public Rigidbody2D playerRB;        // اسحب اللاعب هنا (عشان نوقفه وقت الكلام)

    private string[] dialogueSentences;
    private int index = 0;

    void Start()
    {
        // إخفاء الحوار والزر عند بداية اللعبة
        if (dialogueBox != null) dialogueBox.SetActive(false);
        if (continueButton != null) continueButton.SetActive(false);
    }

    public IEnumerator TypeDialogue()
    {
        // 1. إظهار الصندوق
        dialogueBox.SetActive(true);

        // 2. تجميد حركة اللاعب
        if (playerRB != null)
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        // 3. كتابة النص حرفًا بحرف
        foreach (char letter in dialogueSentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // 4. إظهار زر المتابعة لما الجملة تخلص
        if (continueButton != null) continueButton.SetActive(true);
    }

    public void SetSentences(string[] sentences)
    {
        this.dialogueSentences = sentences;
    }

    // هذه هي الدالة التي يجب وضعها في الزر
    public void NextSentence()
    {
        if (continueButton != null) continueButton.SetActive(false);

        if (index < dialogueSentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TypeDialogue());
        }
        else
        {
            // إنهاء الحوار
            textDisplay.text = "";
            if (dialogueBox != null) dialogueBox.SetActive(false);

            // إعادة حركة اللاعب
            if (playerRB != null)
            {
                // إزالة التجميد مع الحفاظ على منع الدوران
                playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            // تصفير العداد
            index = 0;
            dialogueSentences = null;
        }
    }
}