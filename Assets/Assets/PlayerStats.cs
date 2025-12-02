using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 3;
    public int lives = 3;

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    private SpriteRenderer sr;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void SpriteFlicker()
    {
        flickerTime += Time.deltaTime;
        if (flickerTime >= flickerDuration)
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;

            if (health < 0)
                health = 0;

            if (health == 0)
            {
                if (lives > 0)
                {
                    lives--;
                    FindObjectOfType<LevelManager>().RespawnPlayer();
                }
                else
                {
                    Debug.Log("Game Over");
                    Destroy(gameObject);
                }
            }

            Debug.Log("Player Health: " + health);
            Debug.Log("Player Lives: " + lives);

            isImmune = true;
            immunityTime = 0f;
            flickerTime = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;

            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }
    }
}
