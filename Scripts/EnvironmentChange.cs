using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 *
 * Used to change the environment Sprites. Launched by collider and with button C.
 * Not finished yet. Is needed without collider to activate.
 * 
 */

public class EnvironmentChange : MonoBehaviour
{
    public GameObject normal;
    public GameObject changed;
    public int time;

    private void Start()
    {
        /* Normal environment Sprite is activated when game starts.
         * And Changed environment Sprite is deactivated as default.
         */

        normal.SetActive(true);
        changed.SetActive(false);
        time = 0;
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        /* After Collider has launched the code, this if-statement will be executed for a terminated time before returning Sprites back to their default setting. */

        if(changed.active == true){
            if (time >= 500)
            {
                Debug.Log("Lohko 2");
                normal.SetActive(true);
                changed.SetActive(false);

                time = 0;
            }

            time += 1;
        }
    }

    [System.Obsolete]
    void OnTriggerStay2D(Collider2D other)
    {
        /* Collider launches the change when C is pressed within the collider. */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null && Input.GetKey("c"))
        {
            Debug.Log("C pressed");

            if (normal.active == true)
            {
                /* Normal sprite is deactivated and Changed sprite is activated. Using this, costs player health. */

                Debug.Log("Lohko 1");
                normal.SetActive(false);
                changed.SetActive(true);

                controller.ChangeHealth(-1);
            }
        }
    }

}
