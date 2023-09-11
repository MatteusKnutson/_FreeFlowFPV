using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    string controllerName;

    public static List<AxisHandler> inputChannels;
    
   
    AxisHandler yaw = new("yaw", -1, 0, 1000);
    AxisHandler pitch = new("pitch", -1, 0, 1000);
    AxisHandler roll = new("roll", -1, 0, 1000);
    AxisHandler reset = new("reset", -1, 0, 1000);
    AxisHandler turtle = new("turtle", -1, 0, 1000);


    public static string defaultAxisName = "Axis";

    public static float GetInputValue(string defaultAxisName, int axis)
    { 
        // Gets the raw input data from the selected channel
        return Input.GetAxisRaw(defaultAxisName + axis);
    }

    public static void SetMinValue(AxisHandler channel, int value)
    {
        // Sets the convertion min value
        channel.axualMinValue = value;
    }

    public static void SetMaxValue(AxisHandler channel, int value)
    {
        // Sets the convertion max value
        channel.axualMaxValue = value;
    }

    public static void SetAxisValue(AxisHandler channel, int value)
    {
        // Sets the correct axis to the channel
        channel.axisIndex = value;
    }

    public static float GetConvertedInput(float input, int actualMinValue, int actualMaxValue)
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
            new("throttle", -1, 0, 1000),
            new("yaw", -1, 0, 1000),
            new("pitch", -1, 0, 1000),
            new("roll", -1, 0, 1000),
            new("reset", -1, 0, 1000),
            new("turtle", -1, 0, 1000)
        };
        

    }

    private void Update()
    {

    }
}
