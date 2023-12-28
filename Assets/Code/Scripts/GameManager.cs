using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public UIDocument controlsUI;


    // ----- TEMPORARY CODE ------ //

    void OpenControlsWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlsUI.isActiveAndEnabled) 
            {
                controlsUI.gameObject.SetActive(false);
            }
            else
            {
                controlsUI.gameObject.SetActive(true);
            }
        }
    }

    // ----- /TEMPORARY CODE ------ //



    void Start()
    {
        controlsUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OpenControlsWindow();
    }
}
