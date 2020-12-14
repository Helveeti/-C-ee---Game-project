using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Used as TargetCollider, which objects are pushed into.
 * In this case, TutorialScene´s footsteps that lead you to stone puzzle.
 *
 */

public class TargetObject : MonoBehaviour
{
    private TutorialSceneController ctrl;

    private void Start()
    {
        /* Getting and setting instance of TutorialSceneController. */

        ctrl = TutorialSceneController.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* When FootSteps meets the collider, will code be executed.
         * Freezes FootSteps, which means that sprites are deactivated.
         * TutorialSceneController is being called and stone puzzle is activated.
         * Destroys Footstep GameObject.
         * Destroys itself.
         */

        FootStepsScript controller = other.GetComponent<FootStepsScript>();

        if (controller != null)
        {
            controller.freeze();
            ctrl.setActive();
            Destroy(controller);
            Destroy(this);
        }
    }
}
