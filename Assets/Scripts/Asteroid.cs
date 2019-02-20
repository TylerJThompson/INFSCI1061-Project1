using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;
    public float maxSpinSpeed;
    public Sprite[] spriteArray = new Sprite[4];
    public string size;

    private Pooler mediumPool;
    private Pooler smallPool;

    private SpriteRenderer renderer;
    private Rigidbody2D rb;

    private void Start()
    {
        Pooler[] poolerArray = FindObjectsOfType<Pooler>();
        for (int i = 0; i < poolerArray.Length; i++)
        {
            if (poolerArray[i].name.Equals("Medium Pooler")) mediumPool = poolerArray[i];
            else if (poolerArray[i].name.Equals("Small Pooler")) smallPool = poolerArray[i];
        }
    }

    void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-1f, 0f) * speed);

        int randImg = Random.Range(0, spriteArray.Length);
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = spriteArray[randImg];

        float spinSpeed = Random.Range(-1f * maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(spinSpeed);
    }

    private void Update()
    {
        //if (rb.velocity.x <= 0f) rb.AddForce(new Vector2(-1f, 0f) * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag.Equals("Asteroid") && collision.collider.tag.Equals("Player"))
        {
            if (size.Equals("huge") || size.Equals("big")) BreakAsteroid();
            rb.velocity = new Vector2(0f, 0f);
            rb.angularVelocity = 0f;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag.Equals("Asteroid"))
        {
            if (collision.CompareTag("Projectile"))
            {
                if (size.Equals("huge") || size.Equals("Big")) BreakAsteroid();
                rb.velocity = new Vector2(0f, 0f);
                rb.angularVelocity = 0f;
                gameObject.SetActive(false);
            }
            else if (collision.CompareTag("Respawn"))
            {
                rb.velocity = new Vector2(0f, 0f);
                rb.angularVelocity = 0f;
                gameObject.SetActive(false);
            }
        }
    }

    private void BreakAsteroid()
    {
        if (size.Equals("huge"))
        {
            int numTimes = Random.Range(2, 5);
            for (int i = 0; i < numTimes; i++)
            {
                GameObject medium = mediumPool.getPooledObject();
                if (medium != null)
                {
                    float xOffset = Random.Range(-1f, 1f);
                    float yOffset = Random.Range(-1f, 1f);
                    medium.transform.position = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
                    medium.SetActive(true);
                }
            }
        }
        else
        {
            int numTimes = Random.Range(2, 5);
            for (int i = 0; i < numTimes; i++)
            {
                GameObject small = smallPool.getPooledObject();
                if (small != null)
                {
                    float xOffset = Random.Range(-0.5f, 0.5f);
                    float yOffset = Random.Range(-0.5f, 0.5f);
                    small.transform.position = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
                    small.SetActive(true);
                }
            }
        }
    }
}
