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
        return Input.GetAxisRaw(defaultAxisName + axis);
    }

    public static float GetConvertedInput(float input, AxisHandler channel)
    { 
        // Gets the input which the game is going to use for controlling the drone (the converted input)

        float newValue = ((input - channel.actualMinValue) / (channel.actualMaxValue - channel.actualMinValue) * 2 - 1);
        return newValue;
    }
    
    private void Awake()
    {
        inputChannels = new List<AxisHandler>()
        {
            new("throttle", 3, -1, 1, false),
            new("yaw", 4, -1, 1, false),
            new("pitch", 2, -1, 1, false),
            new("roll", 1, -1, 1, false),
            new("reset", 5, -1, 1, false),
            new("turtle", 6, -1, 1, false)
        };
        

    }

    private void Update()
    {

    }
}
