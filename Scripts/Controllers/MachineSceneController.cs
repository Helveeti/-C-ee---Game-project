using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSceneController
{
    private static MachineSceneController instance = null;

    private bool firstTime, c;

    /* Get instance.
     * To secure that every class is using the same version of the class.
     */
    public static MachineSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MachineSceneController();
            }
            return instance;
        }
    }

    /*
     * Constructor. 
     */
    private MachineSceneController()
    {
        firstTime = true;
    }

    public void noMoreFirstTime() {
        firstTime = false;
    }

    public bool isFirstTime() {
        return firstTime;
    }

    public bool cIsSet() {
        return c;
    }

    public void setC() {
        c = true;
    }

    public void unsetC()
    {
        c = false;
    }


}
