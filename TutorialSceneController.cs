using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *  Used to control over first scene: Tutorial.
 *  Activates it tutorialScene´s features when needed and in right order.
 *  
 *  Has scene Control and Puzzle control.
 *  
 *  Has been made using Singleton design pattern.
 *
 */

public class TutorialSceneController
{
    private bool activePuzzle, puzzleFinished;
    private bool kivi1, kivi2, kivi3, kivi4;
    private static TutorialSceneController instance = null;

    /* Get instance.
     * To secure that every class is using the same version of the class.
     */
    public static TutorialSceneController Instance {
        get
        {
            if (instance == null)
            {
                instance = new TutorialSceneController();
            }
            return instance;
        }
    }

    /* Constructor.
     * Sets variables "false" as default.
     */
    private TutorialSceneController()
    {
        activePuzzle = false;
        puzzleFinished = false;

        kivi1 = false;
        kivi2 = false;
        kivi3 = false;
        kivi4 = false;
    }

    public bool activatePuzzle()
    {
        return activePuzzle;
    }

    public void setActive()
    {
        /* Is called to set Puzzle active or unactive. */

        if (activePuzzle) activePuzzle = false;
        else activePuzzle = true;
    }

    public void setFinished()
    {
        /* Is called to set puzzle to finished.
         * Sets activePuzzle to false.
         */

        if (puzzleFinished) puzzleFinished = false;
        else puzzleFinished = true;

        activePuzzle = false;
    }

    public bool finishedPuzzle()
    {
        return puzzleFinished;
    }

    /* Puzzle determined.
     * 
     * Checks whether other stones are active.
     * If not wrong stones active/unactive, sets stone to active.
     * Otherwise resets all stones.
     * 
     * If all activated in right order, is puzzle set finished.
     */

    public void activeKivi1() {
        if (!(kivi2 && kivi3 && kivi4)) kivi1 = true;
        else resetStones();
    }

    public void activeKivi2() {
        if (!(kivi3 && kivi4) && kivi1) kivi2 = true;
        else resetStones();
    }

    public void activeKivi3() {
        if (!(kivi4) && kivi1 && kivi2) kivi3 = true;
        else resetStones();
    }

    public void activeKivi4() {
        if (kivi1 && kivi2 && kivi3)
        {
            kivi4 = true;
            setFinished();
        }
        else resetStones();
    }

    private void resetStones()
    {
        /* Resets all stones to deactive. */

        kivi1 = false;
        kivi2 = false;
        kivi3 = false;
        kivi4 = false;
    }



}
