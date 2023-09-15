using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    string controllerName;

    public static List<AxisHandler> inputChannels;

    public static string defaultAxisName = "Axis";

    public static float GetInputValue(string defaultAxisName, int axis)
    { 
        // Gets the raw input data from the selected channel
        return Input.GetAxisRaw(defaultAxisName + axis) * 500 + 500;
    }

    public static float GetConvertedInput(float input, float actualMinValue, float actualMaxValue)
    { 
        // Gets the input which the game is going to use for controlling the drone (the converted input)
        if (actualMinValue > actualMaxValue)
        {
            (actualMinValue, actualMaxValue) = (actualMaxValue, actualMinValue);
        }

        float newValue = ((input) - actualMinValue) / (actualMaxValue - actualMinValue) * 1000;
        return newValue;
    }
    
    private void Awake()
    {
        inputChannels = new List<AxisHandler>()
        {
            new("throttle", 3, 0, 1000),
            new("yaw", 4, 0, 1000),
            new("pitch", 2, 0, 1000),
            new("roll", 1, 0, 1000),
            new("reset", 5, 0, 1000),
            new("turtle", 6, 0, 1000)
        };
        

    }

    private void Update()
    {

    }
}
