using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *
 * Used to create multiple line conversations with NPC's, with name tag and Sprites used.
 * You determine every line with assigned sprite and name. You must do this or line, name and sprite will not match.
 * 
 */

public class NPCScript : MonoBehaviour
{
    public string[] line;
    public string[] nimi;
    public Text textKupla;
    public Text nimiKupla;
    public GameObject background;
    public GameObject[] sprites;
    public bool repeatable;

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
         * TextFields are reseted to empty.
         * Background sprite is set deactive.
         * Sprites are reseted until further call.
         * Active calling is set not activated.
         * Variables has been set.
         */

        letters = line[0].ToCharArray();
        textKupla.text = "";
        nimiKupla.text = "";
        background.SetActive(false);
        resetSprites();
        active = false;
        timer = 0f; index = 0; lineRound = 0;
    }

    private void Update()
    {
        /* If conversation is activated from collider, further code will execute.
         *
         * Player is called to be set Freezed.
         * Name is printed to view and background sprite is activated.
         * Speaker sprite is also activated.
         * 
         * Then line is printed by letter, with small delay that creates writing effect.
         * If you want to speed up the writing, you can press space.
         * If text is printed out and you press space:
         *          - If there´s still lines, is next line converted to letters and new sprite and name will be refreshed.
         *          - If there´s no lines left, the code checks if conversation has been set repeatable or not.
         *                  - If it´s set repeatable, will code be destroyed in the end.
         *                  - Otherwise code will be only reseted and you can go trough same conversation as many times as you like.
         * 
         */

        if (active)
        {
            player.SetFreeze(true);
            nimiKupla.text = nimi[lineRound];
            background.SetActive(true);
            sprites[lineRound].SetActive(true);

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
                    sprites[lineRound - 1].SetActive(false);
                    index = 0;
                    textKupla.text = "";
                    nimiKupla.text = "";
                    tempLine = null;
                    letters = null;
                    letters = line[lineRound].ToCharArray();
                }else if(lineRound >= line.Length)
                {
                    textKupla.text = "";
                    nimiKupla.text = "";
                    player.SetFreeze(false);
                    background.SetActive(false);
                    resetSprites();
                    active = false;

                    if (repeatable) {
                        index = 0;
                        lineRound = 0;
                        tempLine = null;
                        letters = null;
                        letters = line[lineRound].ToCharArray();
                    }else Destroy(this);
                }
            }
            else if (Input.GetKey("space") && index < letters.Length)
            {
                timer = -1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* Sets conversation active when Player meets collider. */

        player = other.GetComponent<MainCharacterController>();

        if (player != null)
        {
            active = true;
        }
    }

    private void resetSprites()
    {
        /* Resets all the sprites. */

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(false);
        }
    }
}
