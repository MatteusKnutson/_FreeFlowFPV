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
                
            }
        }
    }

    // ----- /TEMPORARY CODE ------ //



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
