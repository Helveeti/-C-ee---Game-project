using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * TutorialPuzzle script for Stone colliders. 
 * Regocnizes which stone has been pressed and sends information to TutorialSceneController.
 *
 */

public class StoneColliderScript : MonoBehaviour
{
    private TutorialSceneController ctrl;

    private void Start()
    {
        /* Getting and setting instance of TutorialSceneController. */

        ctrl = TutorialSceneController.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* Once player meets collider, determines which stone is class attached to
         * and sends information to TutorialSceneController.
         */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            if (this.CompareTag("Patsas2"))
            {
                ctrl.activeKivi1();
            }
            else if (this.CompareTag("Patsas3"))
            {
                ctrl.activeKivi2();
            }
            else if (this.CompareTag("Patsas4"))
            {
                ctrl.activeKivi3();
            }
            else if (this.CompareTag("Patsas5"))
            {
                ctrl.activeKivi4();
            }
        }
    }
}
