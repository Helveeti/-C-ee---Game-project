using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *
 * Used for catalog dialogue, which doesn´t involved any name tags or sprites. Launched by collider.
 * 
 */

public class ColliderTextScript : MonoBehaviour
{
    public string[] line;
    public Text textKupla;
    public GameObject background;

    private MainCharacterController player;
    private bool active;
    private float timer;
    private char[] letters;
    private int index, lineRound;
    private string tempLine;

    private void Start()
    {
        /* 
         * First line is converted to letters.
         * TextField is set empty.
         * Background is deactivated.
         * Variables is set.
         */

        letters = line[0].ToCharArray();
        textKupla.text = "";
        background.SetActive(false);
        active = false;
        timer = 0f; index = 0; lineRound = 0;
    }

    private void Update()
    {
        /* 
         * If conversation is activated from collider, further code will execute.
         * 
         * Player is called to be set Freezed.
         * Background sprite is activated.
         * 
         * Then line is printed by letter, with small delay that creates writing effect.
         * If you want to speed up the writing, you can press space.
         * 
         * If text is printed out and you press space:
         *          - If there´s still lines, is next line converted to letters and will be printed next.
         *          - If there´s no lines left, the code resets and code destroys itself.
         *
         */

        if (active)
        {
            player.SetFreeze(true);
            background.SetActive(true);

            timer -= 0.1f;
            if (timer <= 0 && index < letters.Length)
            {
                timer += 1f;
                tempLine += letters[index];
                textKupla.text = tempLine;
                index += 1;
            }
            else if (Input.GetKey("space") && index >= letters.Length)
            {
                lineRound += 1;

                if (lineRound < line.Length)
                {
                    index = 0;
                    textKupla.text = "";
                    tempLine = null;
                    letters = null;
                    letters = line[lineRound].ToCharArray();
                }
                else if (lineRound >= line.Length)
                {
                    textKupla.text = "";
                    player.SetFreeze(false);
                    background.SetActive(false);
                    Destroy(gameObject);
                }

            }
            else if (Input.GetKey("space") && index < letters.Length) {
                timer = -1;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        /* Sets conversation active when Player meets collider. */

        player = other.GetComponent<MainCharacterController>();

        if (player != null)
        {
            active = true;
        }
    }
}
