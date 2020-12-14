using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * Controls tutorialPuzzle´s steps over the stones. Doesn´t execute before it´s activated by TutorialSceneController.
 * Once Puzzle is marked as finished, will open the port.
 *
 */

public class StoneStepScript : MonoBehaviour
{
    private TutorialSceneController controller;
    private FootStepCycleScript stepCycle;
    private float stepTimer, nextStep;
    private int round, size;
    private bool roundBreak, freeze, finished;

    public GameObject porttiKiinni;
    public GameObject porttiAuki;

    public GameObject[] steps;

    void Start()
    {
        /*
         * Creates class object FootStepCycleScript().
         * Getting and setting instance of TutorialSceneController.
         * Resets stepSprites.
         * Defines other variables.
         */

        stepCycle = new FootStepCycleScript(steps);
        controller = TutorialSceneController.Instance;
        stepCycle.resetSprites(1);
        stepTimer = 0f; nextStep = 0f;
        size = steps.Length;
        round = 0;
        roundBreak = false; freeze = false; finished = false;
    }

    private void Update()
    {
        /* Checks if puzzle is activated.
         * Checks if puzzle is finished. 
         */

        freeze = controller.activatePuzzle();
        finished = controller.finishedPuzzle();
    }

    void FixedUpdate()
    {
        /* If Freeze has been set as in "true", will further code execute.
         * 
         * StepTimers will start taking time.
         * 
         * if round has been executed, will steps wait a bit and execute idle step twice, before proceeding for new round.
         * else executes new round -
         *      - if there´s round left, change to next Sprite.
         *      - else activate roundBreak.
         *      
         * If Freeze is not activated, check if puzzle has been finished.
         *      - If finished, - reset Sprites and destroy port and it´s wall collider. Activate opened port that will let player trough.
         */

        if (freeze)
        {
            stepTimer += 0.1f;
            nextStep += 0.1f;

            if (roundBreak)
            {
                if (nextStep >= 15f) roundBreak = false;
                else if (stepTimer >= 4f)
                {
                    stepCycle.idleStep();
                    stepTimer = 0f;
                }
            }
            else
            {

                if (round < size)
                {
                    if (nextStep >= 5f)
                    {
                        stepCycle.nextStep();
                        nextStep = 0f;
                        round += 1;
                    }
                }
                else
                {
                    roundBreak = true;
                    round = 0;
                    stepCycle = new FootStepCycleScript(steps);
                }
            }
        }
        else if (finished) {
            stepCycle.resetSprites(0);
            Destroy(porttiKiinni);
            porttiAuki.SetActive(true);
        }
    }
}
