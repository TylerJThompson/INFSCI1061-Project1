using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    bool growing;
    Vector3 change;

    // Start is called before the first frame update
    void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-1f, 0f) * speed;

        growing = true;
        int scale = Random.Range(1, 11);
        gameObject.transform.localScale = new Vector3(scale, scale, 1f);
        change = new Vector3(0.2f, 0.2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.x >= 9f) growing = false;
        else if (gameObject.transform.localScale.x <= 1f) growing = true;

        if (growing) gameObject.transform.localScale += change;
        else gameObject.transform.localScale -= change;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn") || collision.CompareTag("Player"))
        {
            rb.velocity = new Vector2(0f, 0f);
            gameObject.SetActive(false);
        }
    }
}
