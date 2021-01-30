using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *  Used to ease reusing FootStep animation.
 *  Class is being called outside and used as class object.
 *
 */

public class FootStepCycleScript
{
    private GameObject[] steps;
    private int index;

    /* Constructor - Given GameObject array as paramter */
    public FootStepCycleScript(GameObject[] steps) {
        this.steps = steps;
        index = 0;

        resetSprites(1);
    }

    public void nextStep()
    {
        /* Deactivates step sprite, then adds to index.
         * Checks if index is within range, if not, resets it.
         * Activates next sprite.
         */

        steps[index].SetActive(false);
        index += 1;
        if (index >= steps.Length) index = 0;

        steps[index].SetActive(true);

    }

    public void idleStep()
    {
        /* If step is active, deactivates it.
         * If step is inactive, activates it.
         */

        if (steps[index].active == true) steps[index].SetActive(false);
        else steps[index].SetActive(true);
    }

    public void resetSprites(int i)
    {
        /* Reset sprites from giving index. Usually 1 or 0. */

        for (int j = i; j < steps.Length; j++)
        {
            steps[j].SetActive(false);
        }
    }
}
