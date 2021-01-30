using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Used to ease reusage of WalkCycle.
 * Created and used as class object.
 *
 */

public class WalkCycleScript
{
    private GameObject[] runUp;
    private GameObject[] runDown;
    private GameObject[] runRight;
    private GameObject[] runLeft;

    private int count, time;

    private CountingScript c;

    /* Constructor. Given four GameObject arrays as parameters. */
    public WalkCycleScript(GameObject[] runUp, GameObject[] runDown, GameObject[] runRight, GameObject[] runLeft) {
        this.runUp = runUp;
        this.runDown = runDown;
        this.runLeft = runLeft;
        this.runRight = runRight;

        count = 0; time = 0;
        c = new CountingScript();
    }

    public void setStartingPos(GameObject[] obj)
    {
        /* Starting position can be called outside and set for desired direction.
         * Takes GameObject array as parameter.
         */

        obj[count].SetActive(true);
    }

    private void Start()
    {
        /* Set everyother sprite unactive. */

        Deactive(runUp);
        Deactive(runDown);
        Deactive(runRight);
        Deactive(runLeft);
    }

    public void Deactive(GameObject[] obj) {
        /* Method that deactivates all sprited from Array. */

        for (int i = 0; i < obj.Length; i++) {
            obj[i].SetActive(false);
        }
    }

    public void changeSprite(Vector2 position, Vector2 targetPosition)
    {
        /* Changes the sprite by direction.
         * Takes two Vectors as parameters and compares them to define direction.
         */

        time += 1;

        if (time >= 10) {

            float x = c.countDiversition(targetPosition.x, position.x);
            float y = c.countDiversition(targetPosition.y, position.y);

            if (y > x)
            {
                if (targetPosition.y < position.y)
                {
                    /* If player is moving up: */

                    SetSprite(runUp);
                    Deactive(runDown);
                    Deactive(runLeft);
                    Deactive(runRight);

                }
                else if (targetPosition.y > position.y)
                {
                    /* If player is moving down: */

                    SetSprite(runDown);
                    Deactive(runUp);
                    Deactive(runLeft);
                    Deactive(runRight);

                }
            }
            else if (x > y) {
                if (targetPosition.x > position.x)
                {
                    /* If player is moving left: */

                    SetSprite(runLeft);
                    Deactive(runUp);
                    Deactive(runDown);
                    Deactive(runRight);

                }
                else if (targetPosition.x < position.x)
                {
                    /* If player is moving right: */

                    SetSprite(runRight);
                    Deactive(runUp);
                    Deactive(runDown);
                    Deactive(runLeft);

                }
            }/* Idle - script here */

            time = 0;
        }

    }

    private void SetSprite(GameObject[] obj)
    {
        /* Deactivates sprite that has been activated.
         * Goes to next index and if it´s within the range, will set sprite active.
         * If not within the range, resets index.
         */

        obj[count].SetActive(false);
        count += 1;
        if (count >= obj.Length) count = 0;
        obj[count].SetActive(true);
    }
}
