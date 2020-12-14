using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Used in TutorialScene, in footsteps which lead you to Stone puzzle.
 * Rigidbody2D is needed code to work.
 * 
 */

public class FootStepsScript : MonoBehaviour
{
    public GameObject[] steps;
    private Rigidbody2D stepper;
    private Vector2 position;

    private float horizontal, vertical;
    private float speed = 3.0f, timer;
    private bool next;

    private FootStepCycleScript stepCycle;

    void Start()
    {
        /* Get and set needed Rigidbody components.
         * Other viarables defined.
         * All other sprites is deactivated, expect for the first sprite.
         * FootStepCycleScript is created as class object.
         */

        stepper = GetComponent<Rigidbody2D>();
        timer = 0f;
        next = false;

        for (int i = 1; i < steps.Length; i++) {
            steps[i].SetActive(false);
        }

        stepCycle = new FootStepCycleScript(steps);
    }

    void Update()
    {
        /* Updates the directions into which object is moved to. */

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        /* Determines the speed of sprite change.
         * If next is set true, will change the next Sprite.
         * Otherwise will call for idleStep.
         */

        timer += 0.1f;

        if (timer >= 2.5f)
        {
            timer = 0f;

            if (next) { 
                stepCycle.nextStep();
                stepper.MovePosition(position); 
            }
            else stepCycle.idleStep();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* When Player meets the collider, steps will be moving forward.
         * Steps cannot move backwards.
         */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            position = stepper.position;
            Vector2 temp = position;
            position.y = position.y + speed * vertical;

            if (position.y >= temp.y) {
                next = true;
            }
        }
    }

    public void freeze() {
        /* Is called when sprites are needed to deactivate. */

        stepCycle.resetSprites(0);
    }
}
