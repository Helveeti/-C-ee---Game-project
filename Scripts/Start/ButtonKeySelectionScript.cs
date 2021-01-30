using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class ButtonKeySelectionScript : MonoBehaviour
{
    public GameObject[] eye;

    private int index;

    private void Start()
    {
        for (int i = 1; i < eye.Length; i++) {
            eye[i].SetActive(false);
        }
        index = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && index < 2)
        {
            eye[index].SetActive(false);
            index += 1;
            eye[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && index > 0)
        {
            eye[index].SetActive(false);
            index -= 1;
            eye[index].SetActive(true);
        }
        else if (Input.GetKeyDown("space") || Input.GetKeyDown("enter"))
        {
            if (index == 0)
            {
                Loader.Load(Loader.Scene.TutorialScene);
            }
            else if (index == 1)
            {
                Debug.Log("Load pressed");
            }
            else if (index == 2) {
                Application.Quit();
            }
        }
    }
}
