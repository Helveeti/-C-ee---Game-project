using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Loader;

/*
 * 
 * Used to attach to the doors and set with boolean which room will it lead to. Collider launches.
 * Uses class Loader to add the LoadingScene between loads.
 *
 */

public class DoorScript : MonoBehaviour
{
    public bool nextGarden, nextMachine, nextMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* When player meets collider, checks which bool is true.
         * Bool determines destination.
         */

        MainCharacterController controller = collision.GetComponent<MainCharacterController>();

        if(controller != null)
        {
            if (nextGarden)
            {
                Loader.Load(Loader.Scene.GardenScene);
            }
            else if (nextMachine)
            {
                Loader.Load(Loader.Scene.Konehuone);
            }
            else if (nextMusic) {
                Loader.Load(Loader.Scene.Musiikkihuone);
            }
        }
    }
}
