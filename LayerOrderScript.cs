using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used for changing environment Sprite´s layer by Player´s y-position. Collider laucher based.
 * If player is in front of object, object layer is one less than player´s, - in other words, behind the player.
 * If player is behind the object, object layer is one more than player´s, - in other words, in front of player.
 */

public class LayerOrderScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rigidbody;
    private Vector2 position;

    private void Start()
    {
        /* Getting and setting object components. */

        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        sprite.sortingOrder = 5;
    }

    private void Update()
    {
        /* Updating object´s position. */

        position = rigidbody.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        /*
         *  When meeting collider with Player, calls for method changeLayer().
         */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            changeLayer(controller.getPosition(), controller.getLayer());
        }
    }

    public void changeLayer(Vector2 playerPos, int playerLayer)
    {
        /* Compares object´s and Player´s positions and determines object´s layer accordingly. */

        if (playerPos.y > position.y)
        {
            sprite.sortingOrder = playerLayer + 1;
        }
        else if (playerPos.y < position.y)
        {
            sprite.sortingOrder = playerLayer - 1;
        }
        else {
            sprite.sortingOrder = playerLayer + 1;
        }
    }
}
