using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_InputManagerScript : MonoBehaviour
{
    public ProgressBar pgb_axisValue;
    public Button btn_setMin;
    public Button btn_setMax;
    public DropdownField drp_axisChoices;

    bool SetCorrectUIComponents()
    {
        // Pairs the correct ui elements to the code
        try
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            pgb_axisValue = root.Q<ProgressBar>("AxisValue");
            btn_setMin = root.Q<Button>("SetMin");
            btn_setMax = root.Q<Button>("SetMax");
            drp_axisChoices = root.Q<DropdownField>("AxisChoices");
        }
        catch
        {
            return false;
        }
        return true;
    }

    void ReloadAxesChoices()
    {
        // Reloads the list of axis choices
        drp_axisChoices.choices.Clear();
        for (int i = 0; i < InputManager.inputAxes.Count; i++)
        {
            drp_axisChoices.choices.Add(InputManager.inputAxes[i].controlledAxis);
        }
    }

    int GetSelectedAxis()
    {
        // Returns the int in the inputAxes list which the user has selected
        string axisName = drp_axisChoices.value;
        for (int i = 0; i < InputManager.inputAxes.Count; i++)
        {
            if(axisName == InputManager.inputAxes[i].controlledAxis)
            {
                return i;
            }
        }
        return -1;
    }

    void PairButtons()
    {
        // Pairs the set min and max buttons to the correct method.
        btn_setMin.clicked += () => InputManager.SetMinValue(InputManager.inputAxes[GetSelectedAxis()], (int)InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedAxis()));
        btn_setMax.clicked += () => InputManager.SetMinValue(InputManager.inputAxes[GetSelectedAxis()], (int)InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedAxis()));
    }

    void ShowCorrectValue()
    {
        // Shows the correct value in the progressbar
        pgb_axisValue.value = InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedAxis()), InputManager.inputAxes[GetSelectedAxis()].axualMinValue, InputManager.inputAxes[GetSelectedAxis()].axualMaxValue);
        pgb_axisValue.title = (int)InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedAxis()), InputManager.inputAxes[GetSelectedAxis()].axualMinValue, InputManager.inputAxes[GetSelectedAxis()].axualMaxValue) + "/1000";
    }

    void Start()
    {
        
        SetCorrectUIComponents();
        ReloadAxesChoices();
        PairButtons();
        
    }

    void Update()
    {
        ShowCorrectValue();
        


    }
}
