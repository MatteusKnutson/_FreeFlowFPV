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






    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pgb_axisValue = root.Q<ProgressBar>("AxisValue");
        btn_setMin = root.Q<Button>("SetMin");
        btn_setMax = root.Q<Button>("SetMax");
        drp_axisChoices = root.Q<DropdownField>("AxisChoices");


        InputManager.SetMinValue(InputManager.inputAxes[0], 19);


        
    }

    // Update is called once per frame
    void Update()
    {

        pgb_axisValue.value = InputManager.GetConvertedInput(InputManager.GetInputValue("Axis", 1), InputManager.inputAxes[0].axualMinValue, 1000);
        pgb_axisValue.title = (int)InputManager.GetConvertedInput(InputManager.GetInputValue("Axis", 1), InputManager.inputAxes[0].axualMinValue, 1000) + "/1000";

        drp_axisChoices.choices.Add(InputManager.inputAxes[0].controlledAxis);
        drp_axisChoices.choices.Add(InputManager.inputAxes[1].controlledAxis);
        drp_axisChoices.choices.Add(InputManager.inputAxes[2].controlledAxis);

    }
}
