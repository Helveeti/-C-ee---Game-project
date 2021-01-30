using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Basic animation for Script. Can be used as idle animation for environment or character.
 *
 */

public class AnimationScript : MonoBehaviour
{
    public GameObject[] anim;
    private int index, time, round, rounds;

    private TutorialSceneController controller;

    void Start()
    {
        index = 0; time = 0; round = 0;
        rounds = anim.Length -1;

        /* Deactivates all added sprites expect the first one - anim[0] */
        for(int i = 1; i < anim.Length; i++)
        {
            anim[i].SetActive(false);
        }

        controller = TutorialSceneController.Instance;
        
    }

    void FixedUpdate()
    {
        /* Checks if it´s time to change Sprite  */

        if (controller.finishedPuzzle()) {
            if (time >= 10 && round < rounds)
            {
                nextChange();
                time = 0;
                round += 1;
            }
            time += 1;
        }
    }

    private void nextChange() {
        /* Deactivates sprite that has been on and then activates next one */

        anim[index].SetActive(false);
        index += 1;
        if (index >= anim.Length) index = 0;
        anim[index].SetActive(true);
    }
}
