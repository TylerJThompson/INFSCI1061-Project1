using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    private float endPosition;
    private float spriteWidth;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spriteWidth = right.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        right.transform.position = new Vector2(right.transform.position.x - spriteWidth, right.transform.position.y);
        endPosition = left.transform.position.x - spriteWidth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePiece(right);
        movePiece(left);

        if (right.transform.position.x <= endPosition)
        {
            resetPiece(right);
        }
        if (left.transform.position.x <= endPosition)
        {
            resetPiece(left);
        }
    }

    void movePiece(GameObject piece)
    {
        piece.transform.Translate(new Vector2(-transform.right.x * (movementSpeed / 10f), 0f));
    }

    void resetPiece(GameObject piece)
    {
        piece.transform.position = new Vector2(piece.transform.position.x + spriteWidth * 2, piece.transform.position.y);
    }
}
