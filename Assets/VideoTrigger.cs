using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;
    private bool isPlayerInRange = false;

    void Update()
    {
        // 1. الشرط: لو اللاعب جوه البوكس + داس على حرف H
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.H))
        {
            PlayVideoAndClearScene();
        }
    }

    void PlayVideoAndClearScene()
    {
        // --- أولاً: إيقاف جميع الأصوات في المشهد (بما فيها الكاميرا) ---
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAudioSources)
        {
            // بنعمل شرط: لو مصدر الصوت ده "مش" هو هو صوت الفيديو.. وقفه
            // (عشان منقفلش صوت الفيديو نفسه بالغلط)
            if (myVideoPlayer.GetComponent<AudioSource>() != audio)
            {
                audio.Stop();
            }
        }

        // --- ثانياً: تدمير اللاعب ---
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        // --- ثالثاً: تشغيل الفيديو ---
        if (myVideoPlayer != null)
        {
            myVideoPlayer.Play();
        }

        // --- رابعاً: تنظيف التريجر ---
        Destroy(GetComponent<BoxCollider2D>());

        // (اختياري) إخفاء أي عناصر UI لو عندك Canvas
        // GameObject.Find("Canvas").SetActive(false); 

        // تعطيل السكربت
        this.enabled = false;
    }

    // لما يدخل البوكس
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // لما يخرج من البوكس
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}