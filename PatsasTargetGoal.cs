using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Used in GardenPuzzle.
 * Is set to statue goal collider and given statue´s tag, so it´s made sure that it will be right statue.
 *
 */

public class PatsasTargetGoal : MonoBehaviour
{
    public string goalTag;
    public GameObject coll;

    void OnTriggerEnter2D(Collider2D other)
    {
        /* When Statue meets collider, checks if the tag is right.
         * Activtes wall collider that prevents you from walking trough statue after that.
         * Destroys MovingStatue object.
         */

        MovingObjectAround controller = other.GetComponent<MovingObjectAround>();

        if (controller != null && controller.CompareTag(goalTag))
        {
            coll.SetActive(true);
            Destroy(controller);
        }
    }
}
