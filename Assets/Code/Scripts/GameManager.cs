using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public UIDocument controlsUI;
    public UIDocument ratesUI;
    public UIDocument droneSettingsUI;


    // Controlls/parents most non-drone related gameplay (ex. gui)

    void OpenWindows()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!controlsUI.isActiveAndEnabled && !ratesUI.isActiveAndEnabled && !droneSettingsUI.isActiveAndEnabled) 
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
            if (!ratesUI.isActiveAndEnabled && !controlsUI.isActiveAndEnabled && !droneSettingsUI.isActiveAndEnabled)
            {
                ratesUI.gameObject.SetActive(true);
            }
            else
            {
                ratesUI.gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!ratesUI.isActiveAndEnabled && !controlsUI.isActiveAndEnabled && !droneSettingsUI.isActiveAndEnabled)
            {
                droneSettingsUI.gameObject.SetActive(true);
            }
            else
            {
                droneSettingsUI.gameObject.SetActive(false);
            }
        }
    }


    void Start()
    {
        controlsUI.gameObject.SetActive(false);
    }

    void Update()
    {
        OpenWindows();
    }
}
