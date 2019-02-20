using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource regularMusic;
    public AudioSource bossMusic;
    public int maxScore;
    public int bossMaxHealth;

    public static GameManager instance = null;
    private static int score;
    private static int health;
    private static int shield;
    private static int bossHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 5;
        shield = 3;
        bossHealth = bossMaxHealth;
    }

    public void incrementHealth()
    {
        shield = Mathf.Min(shield + 1, 3);
    }

    public void decrementHealth()
    {
        if (shield > 0) shield = Mathf.Max(shield - 1, 0);
        else health = Mathf.Max(health - 1, 0);
    }

    public int getHealth()
    {
        return health;
    }

    public int getShield()
    {
        return shield;
    }

    public void incrementScore()
    {
        if (health > 0) score++;
        if (regularMusic.isPlaying && score >= maxScore)
        {
            regularMusic.Stop();
            bossMusic.Play();
        }
    }

    public int getScore()
    {
        return score;
    }

    public void damageBoss()
    {
        bossHealth = Mathf.Max(bossHealth - 1, 0);
    }

    public int getBossHealth()
    {
        return bossHealth;
    }

    public void resetBossHealth()
    {
        bossHealth = bossMaxHealth;
    }
}
