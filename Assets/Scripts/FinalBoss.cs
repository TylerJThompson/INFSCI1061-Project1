using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float speed;
    public GameManager gameManager;
    public Spawner spawner;
    public float spawnInterval;

    bool canBeHurt = false;
    Rigidbody2D rb;
    SpriteRenderer flames;
    bool canExplode = true;

    void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-1f, 0f) * speed);
        foreach (SpriteRenderer sR in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (sR.gameObject.name.Equals("Flames")) flames = sR;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canExplode && gameManager.getBossHealth() <= 0)
        {
            canExplode = false;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().SetBool("explodeBool", true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            spawner.gameObject.SetActive(false);
        }

        if (transform.position.x <= 10)
        {
            rb.velocity = new Vector2(0f, 0f);
            flames.enabled = false;
            canBeHurt = true;
            spawner.setSpawnInterval(spawnInterval);
            spawner.toggleSpawn();
        }
        else gameManager.resetBossHealth();
    }

    public bool CanBeHurt()
    {
        return canBeHurt;
    }
}
