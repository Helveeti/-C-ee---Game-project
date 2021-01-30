using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenSceneController
{
    private static GardenSceneController instance = null;

    private bool puzzleFinished;
    private bool patsas2, patsas3, patsas4, patsas5, patsas6;

    private Vector2 posPatsas2, posPatsas3, posPatsas4, posPatsas5, posPatsas6;

    public static GardenSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GardenSceneController();
            }
            return instance;
        }
    }

    private GardenSceneController() {
        puzzleFinished = false;

        patsas2 = false;
        patsas3 = false;
        patsas4 = false;
        patsas5 = false;
        patsas6 = false;
    }

    public void setFinished()
    {
        puzzleFinished = true;
    }

    public bool finishedPuzzle()
    {
        return puzzleFinished;
    }

    /* Stone puzzle  */

    public void setPatsas(string tag) {
        switch (tag)
        {
            case "Patsas2":
                patsas2 = true;
                break;
            case "Patsas3":
                patsas3 = true;
                break;
            case "Patsas4":
                patsas4 = true;
                break;
            case "Patsas5":
                patsas5 = true;
                break;
            case "Patsas6":
                patsas6 = true;
                break;
        }

        checkPatsaat();
    }

    private void checkPatsaat() {
        if (patsas2 && patsas3 && patsas4 && patsas5 && patsas6) setFinished();
    }

    private void resetPatsaat() {
        patsas2 = false;
        patsas3 = false;
        patsas4 = false;
        patsas5 = false;
        patsas6 = false;
    }

    /* Get postion for stone that has already been set into right place. */

    public void savePosition(string tag, Vector2 position)
    {
        switch (tag)
        {
            case "Patsas2":
                posPatsas2 = position;
                break;
            case "Patsas3":
                posPatsas3 = position;
                break;
            case "Patsas4":
                posPatsas4 = position;
                break;
            case "Patsas5":
                posPatsas5 = position;
                break;
            case "Patsas6":
                posPatsas6 = position;
                break;
        }
    }

    public Vector2 getSavedPosition(string tag)
    {
        Vector2 temp = new Vector2();

        switch (tag)
        {
            case "Patsas2":
                temp = posPatsas2;
                break;
            case "Patsas3":
                temp = posPatsas3;
                break;
            case "Patsas4":
                temp = posPatsas4;
                break;
            case "Patsas5":
                temp = posPatsas5;
                break;
            case "Patsas6":
                temp = posPatsas6;
                break;
        }
        return temp;
    }
}
