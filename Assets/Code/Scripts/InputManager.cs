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

    public static void SetMinValue(AxisHandler channel, float value)
    {
        // Sets the convertion min value
        channel.axualMinValue = value * 500 + 500;
        Debug.Log(value + " set on channel " +  channel.controlledChannel);
    }

    public static void SetMaxValue(AxisHandler channel, float value)
    {
        // Sets the convertion max value
        channel.axualMaxValue = value * 500 + 500;
        Debug.Log(value + " set on channel " + channel.controlledChannel);
    }

    public static void SetAxisValue(AxisHandler channel, int value)
    {
        // Sets the correct axis to the channel
        channel.axisIndex = value;
        Debug.Log(value + " set on channel " + channel.controlledChannel);
    }

    public static float GetConvertedInput(float input, float actualMinValue, float actualMaxValue)
    { 
        // Gets the input which the game is going to use for controlling the drone (the converted input)
        if (actualMinValue > actualMaxValue)
        {
            (actualMinValue, actualMaxValue) = (actualMaxValue, actualMinValue);
        }

        float newValue = ((input * 500 + 500) - actualMinValue) / (actualMaxValue - actualMinValue) * 1000;
        return newValue;
    }

    
    private void Awake()
    {
        inputChannels = new List<AxisHandler>()
        {
            new("throttle", 1, 0, 1000),
            new("yaw", 2, 0, 1000),
            new("pitch", 3, 0, 1000),
            new("roll", 4, 0, 1000),
            new("reset", 5, 0, 1000),
            new("turtle", 6, 0, 1000)
        };
        

    }

    private void Update()
    {

    }
}
