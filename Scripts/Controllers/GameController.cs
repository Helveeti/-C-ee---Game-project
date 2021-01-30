using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private static GameController instance = null;
    private GardenSceneController gardenCtrl;
    private MachineSceneController machineCtrl;
    // Music room controller

    private bool gardenFinished;
    // Music room finished

    private string room, lastRoom;
    private int[,] startingPoints;
    private int x, y;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }

    private GameController() {
        gardenCtrl = GardenSceneController.Instance;
        machineCtrl = MachineSceneController.Instance;

        gardenFinished = false;

        int[,] startingPoints = new int[,] {
                                { 0, 0 }, { 0, 84 }, /* Tutorial scene. 0 - 1 */
                                { 0, -7 }, { 9, 6 }, /* Machine scene. 2 - 3 */
                                { 227, 91 }, { 216, 210}, /* Music scene. 4 - 5 */
                                { 8, -12 } /* Garden scene. 6 */
        };
        x = 0; y = 0;
        lastRoom = "";
    }

    public bool checkProgress() {
        gardenFinished = gardenCtrl.finishedPuzzle();

        if (gardenFinished) return true;
        else return false;
    }

    public void cIsSet() {
        Debug.Log("C is pressed in room: " + room);

        if (room.Equals("Machine")) {
            machineCtrl.setC();
        }
    }

    /* Remembering what last room was, and therefore starting the game from right spot in next room. */

    public void setRoom(string s)
    {
        room = s;
    }

    public void setStartingPosition()
    {
        x = 0; y = 0;

        switch (room)
        {
            case "Tutorial":
                x = 0;
                y = 0;
                break;
            case "Machine":
                if (lastRoom.Equals("Music"))
                {
                    x = 9;
                    y = 6;
                }
                else
                {
                    x = 0;
                    y = -7;
                }
                break;
            case "Music":
                if (lastRoom.Equals("Garden"))
                {
                    x = 227;
                    y = 210;
                }
                else
                {
                    x = 227;
                    y = 91;
                }
                break;
            case "Garden":
                x = 8;
                y = -12;
                break;
        }

        lastRoom = room;
    }

    public int getStartingX()
    {
        return x;
    }

    public int getStartingY()
    {
        return y;
    }
}
