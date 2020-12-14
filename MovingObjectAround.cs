using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *  Used to push movingObjects around.
 *  Object needs to have Rigidbody this to work.
 *
 */

public class MovingObjectAround : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal, vertical;
    public float speed = 3.0f;

    void Start()
    {
        /* Gets component rigidbody. */

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /* Updates the directions into which object is moved to. */

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* When Player meets the collider, movingObject will be moving desired direction. */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal;
            position.y = position.y + speed * vertical;

            rigidbody2d.MovePosition(position);
        }
    }
}
