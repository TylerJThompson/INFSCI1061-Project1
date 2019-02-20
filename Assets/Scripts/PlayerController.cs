using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Pooler projectilePooler;
    public Pooler explosionPooler;
    public GameManager gameManager;

    Rigidbody2D rb;
    Quaternion initialRotation;
    float initialX;
    AudioSource laser, recharge;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        
        //Sets rb to the RigidBody2D of this object
        rb = gameObject.GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation;
        initialX = transform.position.x;
        foreach (AudioSource audio in gameObject.GetComponents<AudioSource>())
        {
            if (audio.clip.name.Equals("Laser")) laser = audio;
            else recharge = audio;
        }
    }

    void Update()
    {
        if (transform.rotation != initialRotation) transform.rotation = initialRotation;
        if (transform.position.x != initialX) transform.position = new Vector2(initialX, transform.position.y);

        if (gameManager.getBossHealth() > 0)
        {
            float ySpeed = 0f;

            //Get speed by input - snappy
            if (Input.GetKey(KeyCode.W)) ySpeed = 1f;
            else if (Input.GetKey(KeyCode.S)) ySpeed = -1f;

            //Add force by axis - floaty
            rb.AddForce(new Vector2(0f, ySpeed) * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                laser.Play();
                spawnProjectile();
            }
        }
    }

    void spawnProjectile()
    {
        GameObject projectile = projectilePooler.getPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            projectile.SetActive(true);
        }
    }

    void spawnExplosion(Vector2 pos)
    {
        GameObject explosion = explosionPooler.getPooledObject();
        if (explosion != null)
        {
            explosion.transform.position = pos;
            explosion.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Asteroid"))
        {
            spawnExplosion(collision.GetContact(0).point);
            gameManager.decrementHealth();
            if (gameManager.getHealth() == 0)
            {
                spawnExplosion(transform.position);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Token"))
        {
            gameManager.incrementHealth();
            recharge.Play();
        }
    }
}

//Get speed by axis - floaty
//xSpeed = Input.GetAxis("Horizontal");
//ySpeed = Input.GetAxis("Vertical");

//Translate by axis - snappy, but can skip over things due to teleportation
//transform.Translate(new Vector2(xSpeed, ySpeed) * speed);

//Set velocity for movement - snappy without skipping over things, but more computationally expensive
//rb.velocity = new Vector2(xSpeed, ySpeed) * speed;
