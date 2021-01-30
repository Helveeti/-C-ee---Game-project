using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *  Used to push movingObjects around.
 *  Object needs to have Rigidbody this to work.
 *
 */

public class MovingObjectAround : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal, vertical;
    public float speed = 3.0f;

    public bool x, y;
    public float goalPosition;

    private Vector2 startingPos;
    private float temp;

    private CountingScript count;
    private GardenSceneController ctrl;

    void Start()
    {
        count = new CountingScript();
        ctrl = GardenSceneController.Instance;

        /* Gets component rigidbody. */

        rigidbody2d = GetComponent<Rigidbody2D>();
        startingPos = rigidbody2d.position;

        if (ctrl.finishedPuzzle() && this.tag != "Patsas3")
        {
            startingPos = ctrl.getSavedPosition(this.tag);
            rigidbody2d.MovePosition(startingPos);
        }

        if(x) temp = count.countDiversition(goalPosition, startingPos.x);
        else temp = count.countDiversition(goalPosition, startingPos.y);
    }

    void Update()
    {
        /* Updates the directions into which object is moved to. */

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* When Player meets the collider, movingObject will be moving desired direction. */

        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            Vector2 position = rigidbody2d.position;
            Vector2 targetPosition = position;
            targetPosition.x = position.x + speed * horizontal;
            targetPosition.y = position.y + speed * vertical;

            float res = 0f;

            if (x) res = count.countDiversition(goalPosition, targetPosition.x);
            else res = count.countDiversition(goalPosition, targetPosition.y);

            RaycastHit2D raycast = Physics2D.Raycast(position, targetPosition, 1);
            if (raycast.collider != null && raycast.collider.CompareTag("Wall"))
            {
                Debug.Log("Wall.");
            }
            else if(res < temp){
                temp = res;
                rigidbody2d.MovePosition(targetPosition);
                ctrl.savePosition(this.tag, targetPosition);
            }
        }
    }


}
