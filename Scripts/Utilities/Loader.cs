using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * Used to change between the Scenes with SceneManagement.
 * Adds Loading Screen between every scene change.
 * Between Loading screen, already starts loading next scene to reduce time and prevent loading failures.
 * 
 * Uses LoaderCallBack class.
 *
 */

public class Loader : MonoBehaviour
{

    /* Determine scenes into which can move into. */
   public enum Scene { 
        LoadingScene,
        Start,
        TutorialScene,
        GardenScene,
        Musiikkihuone,
        Konehuone
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene) {
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }


    public static void LoaderCallback() {
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
