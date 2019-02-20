using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    GameManager gameManager;
    Rigidbody2D rb;
    FinalBoss boss;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1f, 0f) * speed;
        boss = FindObjectOfType<FinalBoss>();
        gameObject.tag = "Projectile";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("Boss"))
        {
            if (collision.CompareTag("Asteroid")) gameManager.incrementScore();
            else if (boss.CanBeHurt()) gameManager.damageBoss();
            rb.velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Explosion";
            gameObject.GetComponent<Animator>().SetBool("hitBool", true);
        }
        else if (collision.CompareTag("Projectile Respawn"))
        {
            rb.velocity = new Vector2(0f, 0f);
            gameObject.SetActive(false);
        }
    }
}
