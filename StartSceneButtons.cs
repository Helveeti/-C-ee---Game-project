using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Start menu. Defines which scene will a button press take you.
 */

public class StartSceneButtons : MonoBehaviour
{
    public bool isStart, isQuit;

    public void sceneChanger()
    {
        /* The method is defined into button and bool defines which Scene it will take you. */

        if (isStart)
        {
            Loader.Load(Loader.Scene.TutorialScene);
        }
        else if (isQuit)
        { 
            Application.Quit();
        }
    }
}
