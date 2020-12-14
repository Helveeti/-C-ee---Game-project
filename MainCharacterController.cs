using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * 
 *  Code used for the PlayerCharacter. Includes logic for moving around the map, call for changing Sprites for the right direction.
 *  Changing the health of player and other player inclusive features.
 *
 */

public class MainCharacterController : MonoBehaviour
{
    public int maxHealth = 5, layer = 5;
    private int currentHealth, time = 0, count = 0;

    private Rigidbody2D rigidbody2d;
    float horizontal, vertical;
    public float speed = 0.075f;
    Vector2 savePosition, resetPosition;
    bool freeze;

    private WalkCycleScript walk;

    public GameObject[] runUp;
    public GameObject[] runDown;
    public GameObject[] runLeft;
    public GameObject[] runRight;

    void Start()
    {
        /* Setting variable defaults.
         * Creating WalkCycleScript class object.
         * And setting starting sprite to face Up.
         */

        rigidbody2d = GetComponent<Rigidbody2D>();
        savePosition = rigidbody2d.position;
        resetPosition = rigidbody2d.position;
        currentHealth = 5;
        freeze = false;
        walk = new WalkCycleScript(runUp, runDown, runRight, runLeft);

        walk.Deactive(runUp);
        walk.Deactive(runDown);
        walk.Deactive(runRight);
        walk.Deactive(runLeft);
        walk.setStartingPos(runUp);
    }

    void Update()
    {
        /* Updating which buttons Player is pressing to move.
         * And possibility to leave game by pressing Esc.
         */

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Esc pressed");
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        /*
         * First we get player's position and then calculate targetPosition with RaycastHit.
         * Then we compare whether is hits collider and then if collider is tagged with "Wall"
         * 
         * if it hits and player hasn´t hit it over 100 times, will player be set back at last saved position.
         * if it hits and player has hit "wall" tagged collider over 100, will player be returned to starting position.
         * 
         * This is to prevent from player getting stuck and need to boot the game.
         * 
         * Then if targetPosition doesn´t hit the collider which is tagged as "Wall", we check if player has been Freezed.
         * If that´s not the case, player we may move to the target point.
         * At this point our Walk class object is called to change the sprite for the right direction.
         * 
         * Then lastly we have timer that saves new last savePosition every 10 ms.
         */

        Vector2 position = getPosition();
        Vector2 targetPosition = position;
        targetPosition.x = position.x + speed * horizontal;
        targetPosition.y = position.y + speed * vertical;

        RaycastHit2D raycast = Physics2D.Raycast(position, targetPosition, 1);
        if (raycast.collider != null && raycast.collider.CompareTag("Wall"))
        {
            Debug.Log("Wall");

            if (count > 100)
            {
                rigidbody2d.MovePosition(resetPosition);
                count = 0;
            }
            else {
                rigidbody2d.MovePosition(savePosition);
                count += 1;
            }
        }
        else if(freeze != true) {
            walk.changeSprite(targetPosition, position);
            rigidbody2d.MovePosition(targetPosition);
        }

        if (time >= 10){
            savePosition = position;
            time = 0;
        }

        time += 1;
    }

    public void ChangeHealth(int amount)
    {
        /* Change the health from outside of the class. */

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void SetFreeze(bool change)
    {
        /* Set player to freeze outside the class.*/
        freeze = change;
    }

    public int getLayer()
    {
        return layer;
    }

    public Vector2 getPosition()
    {
        return rigidbody2d.position;
    }

}
