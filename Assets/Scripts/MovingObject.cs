using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int speed;
    protected Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void movement(int speed)
    {
        rigidBody.velocity = new Vector2(-speed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rigidBody.velocity = new Vector2(0f, 0f);
        rigidBody.angularVelocity = 0f;
        gameObject.SetActive(false);
    }
}
