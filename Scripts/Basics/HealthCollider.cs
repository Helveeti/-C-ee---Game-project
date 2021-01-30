using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Used to change Player´s healt when picking up health item. 
 * Set how much health you want player to recieve.
 *
 */

public class HealthCollider : MonoBehaviour
{
    public int health;

    void OnTriggerEnter2D(Collider2D other)
    {
        /* When Player meets collider, calls for MainCharacterController method ChangeHealth().
         * Destroys HealthObject.
         */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            controller.ChangeHealth(health);
            Destroy(gameObject);
        }
    }
}
