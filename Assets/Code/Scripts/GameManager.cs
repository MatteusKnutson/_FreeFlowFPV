using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public UIDocument controlsUI;
    public UIDocument ratesUI;


    // ----- TEMPORARY CODE ------ //

    void OpenWindows()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!controlsUI.isActiveAndEnabled && !ratesUI.isActiveAndEnabled) 
            {
                controlsUI.gameObject.SetActive(true);
            }
            else
            {
                controlsUI.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!ratesUI.isActiveAndEnabled && !controlsUI.isActiveAndEnabled)
            {
                ratesUI.gameObject.SetActive(true);
            }
            else
            {
                ratesUI.gameObject.SetActive(false);
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
        OpenWindows();
    }
}
