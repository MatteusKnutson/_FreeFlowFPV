using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    string controllerName;

    AxisHandler throttle = new("throttle", -1, 0, 1000);
    AxisHandler yaw = new("yaw", -1, 0, 1000);
    AxisHandler pitch = new("pitch", -1, 0, 1000);
    AxisHandler roll = new("roll", -1, 0, 1000);
    AxisHandler reset = new("reset", -1, 0, 1000);
    AxisHandler turtle = new("turtle", -1, 0, 1000);

    public List<AxisHandler> inputAxes;

    string defaultAxisName = "Axis";

    float GetInputValue(string defaultAxisName, int axis)
    {
        // Gets the raw input data from the selected channel
        return Input.GetAxisRaw(defaultAxisName + axis);
    }

    void SetMinValue(AxisHandler axis, int value)
    {
        // Sets the converted min value
        axis.axualMinValue = value;
    }

    void SetMaxValue(AxisHandler axis, int value)
    {
        // Sets the converted max value
        axis.axualMaxValue = value;
    }

    float GetConvertedInput(float input, int actualMinValue, int actualMaxValue)
    {
        // Gets the input which the game is going to use for controlling the drone (the converted input)
        if (actualMinValue > actualMaxValue)
        {
            (actualMinValue, actualMaxValue) = (actualMaxValue, actualMinValue);
        }

        float newValue = ((input * 500 + 500) - actualMinValue) / (actualMaxValue - actualMinValue) * 1000;
        return newValue;
    }

    
    private void Start()
    {
        inputAxes = new List<AxisHandler>() { throttle, yaw, pitch, roll, reset, turtle };
        
    }

    private void Update()
    {
        Debug.Log(GetConvertedInput(GetInputValue(defaultAxisName, throttle.axisIndex), 19, 900));
    }
}
