using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public int health = 3;
    public int lives = 3;

    private float flickerTimer = 0f;
    private float flickerDuration = 0.1f;

    private SpriteRenderer sr;

    private float invincibilityDuration = 1.5f;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (isInvincible)
        {
            SpriteFlicker();
            invincibilityTimer += Time.deltaTime;

            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                sr.enabled = true;
            }
        }
        scoreUI.text=" " + score;
    }

    public void SpriteFlicker()
    {
        flickerTimer += Time.deltaTime;

        if (flickerTimer >= flickerDuration)
        {
            sr.enabled = !sr.enabled;
            flickerTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            if (health <= 0)
            {
                lives--;
                if (lives > 0)
                {
                    FindObjectOfType<LogicManager>().RespawnPlayer();
                    health = 3;
                    lives--;
                }
            }
        }
        isInvincible = true;
        invincibilityTimer = 0f;
    }



}