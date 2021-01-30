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
    private int currentHealth;

    private Rigidbody2D rigidbody2d;
    private float horizontal, vertical;
    public float speed = 0.075f;
    private bool freeze, firstXHit, firstYHit,
        yDown, yUp, xLeft, xRight;

    private WalkCycleScript walk;
    private CountingScript count;
    private GameController gameCtrl;

    public GameObject[] runUp;
    public GameObject[] runDown;
    public GameObject[] runLeft;
    public GameObject[] runRight;

    public bool roomIsGarden, roomIsTutorial, roomIsMusic, roomIsMachine;

    void Start()
    {
        /* Setting variable defaults.
         * Creating WalkCycleScript class object.
         * And setting starting sprite to face Up.
         */

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = 5;
        freeze = false; firstYHit = false; firstXHit = false; yDown = false;
        yUp = false; xLeft = false; xRight = false;
        walk = new WalkCycleScript(runUp, runDown, runRight, runLeft);
        count = new CountingScript();
        gameCtrl = GameController.Instance;

        walk.Deactive(runUp);
        walk.Deactive(runDown);
        walk.Deactive(runRight);
        walk.Deactive(runLeft);
        walk.setStartingPos(runUp);

        checkRoom();
        setPosition();
    }

    private void setPosition()
    {
        gameCtrl.setStartingPosition();

        Vector2 position = getPosition();
        position.x = gameCtrl.getStartingX();
        position.y = gameCtrl.getStartingY();

        rigidbody2d.MovePosition(position);
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
        else if (Input.GetKeyDown("c")) {
            gameCtrl.cIsSet();
        }

        if (gameCtrl.checkProgress()) {
            //Debug.Log("Puzzles finished.");
        }
    }

    private void FixedUpdate()
    {
        /* !! OUTDATED !! Since 21/12/-20
         * 
         * First we get player's position and then calculate targetPosition with RaycastHit.
         * Then we compare whether is hits collider and then if collider is tagged with "Wall"
         * 
         * if it hits and player hasn´t hit it over 100 times, will player be set back at last saved position.
         * if it hits and player has hit "wall" tagged collider over 100, will player be returned to starting position.
         * 
         * This is to prevent from player getting stuck and need to boot the game.
         * 
         * Then if targetPosition doesn´t hit the collider which is tagged as "Wall", we check if player has been Freezed.
         * If that´s not the case, player may move to the target point.
         * At this point our Walk class object is called to change the sprite for the right direction.
         * 
         * Then lastly we have timer that saves new last savePosition every 10 ms.
         */

        if (!freeze) {

            Vector2 position = getPosition();
            Vector2 targetPosition = position;
            targetPosition.x = position.x + speed * horizontal;
            targetPosition.y = position.y + speed * vertical;

            float x = count.countDiversition(targetPosition.x, position.x);
            float y = count.countDiversition(targetPosition.y, position.y);

            RaycastHit2D raycast = Physics2D.Raycast(position, targetPosition, 1);
            if (raycast.collider != null && raycast.collider.CompareTag("Wall"))
            {
                Vector3 nextPos = position;

                /* Recognise the wall. */

                if (x > y && firstXHit != true)
                {
                    /* X-wall */
                    firstXHit = true;

                    if (targetPosition.x > position.x) {
                        xRight = true;
                    }
                    else
                    {
                        xLeft = true;
                    }
                }
                
                if(y > x && firstYHit != true)
                {
                    /* Y-wall. */
                    firstYHit = true;

                    if (targetPosition.y > position.y)
                    {
                        yUp = true;
                    }
                    else
                    {
                        yDown = true;
                    }
                }

                /* If wall is... Allow moving other than towards wall */

                if (xRight)
                {
                    if (firstYHit) {

                        if (yUp)
                        {
                            if (targetPosition.y < nextPos.y) nextPos.y = targetPosition.y;
                        }else if (yDown)
                        {
                            if (targetPosition.y > nextPos.y) nextPos.y = targetPosition.y;
                        }
                    }

                    if (targetPosition.x < nextPos.x) nextPos.x = targetPosition.x;
                    walk.changeSprite(nextPos, position);
                    rigidbody2d.MovePosition(nextPos);
                }
                else if (xLeft)
                {
                    if (firstYHit)
                    {
                        if (yUp)
                        {
                            if (targetPosition.y < nextPos.y) nextPos.y = targetPosition.y;
                        }
                        else if (yDown)
                        {
                            if (targetPosition.y > nextPos.y) nextPos.y = targetPosition.y;
                        }
                    }

                    if (targetPosition.x > nextPos.x) nextPos.x = targetPosition.x;
                    walk.changeSprite(nextPos, position);
                    rigidbody2d.MovePosition(nextPos);
                }
                else if (yUp)
                {
                    if (firstXHit)
                    {
                        if (xLeft)
                        {
                            if (targetPosition.x > nextPos.x) nextPos.x = targetPosition.x;
                        }
                        else if (xRight)
                        {
                            if (targetPosition.x < nextPos.x) nextPos.x = targetPosition.x;
                        }
                    }

                    if (targetPosition.y < nextPos.y) nextPos.y = targetPosition.y;
                    walk.changeSprite(nextPos, position);
                    rigidbody2d.MovePosition(nextPos);
                }
                else if (yDown)
                {
                    if (firstXHit)
                    {
                        if (xLeft)
                        {
                            if (targetPosition.x > nextPos.x) nextPos.x = targetPosition.x;
                        }
                        else if (xRight)
                        {
                            if (targetPosition.x < nextPos.x) nextPos.x = targetPosition.x;
                        }
                    }

                    if (targetPosition.y > nextPos.y) nextPos.y = targetPosition.y;
                    walk.changeSprite(nextPos, position);
                    rigidbody2d.MovePosition(nextPos);
                }
            }
            else
            {
                if (raycast.collider == null || (raycast.collider != null && !(raycast.collider.CompareTag("Wall"))))
                {
                    xRight = false; xLeft = false;
                    yUp = false; yDown = false;
                    firstYHit = false;
                    firstXHit = false;
                }

                Vector3 temp = position;

                if (x > y)
                {
                    temp.x = targetPosition.x;
                    rigidbody2d.MovePosition(temp);
                }
                else if(x < y) {
                    temp.y = targetPosition.y;
                    rigidbody2d.MovePosition(temp);
                }

                walk.changeSprite(targetPosition, position);
            }
        }

    }

    public void ChangeHealth(int amount)
    {
        /* Change the health from outside of the class. */

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void SetFreeze(bool change)
    {
        /* Set player to freeze outside of the class.*/
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

    private void checkRoom()
    {
        if (roomIsTutorial) gameCtrl.setRoom("Tutorial");
        else if (roomIsMachine) gameCtrl.setRoom("Machine");
        else if (roomIsMusic) gameCtrl.setRoom("Music");
        else if (roomIsGarden) gameCtrl.setRoom("Garden");
    }

}
