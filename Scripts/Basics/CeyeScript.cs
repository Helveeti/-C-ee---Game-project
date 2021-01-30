using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeyeScript : MonoBehaviour
{
    //public GameObject normal;
    public GameObject changed;

    private MachineSceneController ctrl;

    void Start()
    {
        ctrl = MachineSceneController.Instance;

        changed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ctrl.cIsSet())
        {
            changed.SetActive(true);
            ctrl.unsetC();
        }

        
    }
}
