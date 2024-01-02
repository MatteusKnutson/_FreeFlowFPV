using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    string controllerName;

    public static List<AxisHandler> inputChannels;
    public static List<RatesHandler> rates;

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
        if (channel.isInverted)
            newValue *= -1;

        return newValue;
    }

    public static void InvertMinMaxValues(AxisHandler channel)
    {
        // Swap min and max vbalues if min is bigger than max and inverts the channel
        if(channel.actualMinValue > channel.actualMaxValue)
        {
            float y = channel.actualMaxValue;
            channel.actualMaxValue = channel.actualMinValue;
            channel.actualMinValue = y;
            channel.isInverted = true;
        }
        else
        {
            channel.isInverted = false;
        }
    }

    static float Constrainf(float amt, float low, float high)
    {
        if (amt < low)
            return low;
        else if (amt > high)
            return high;
        else
            return amt;
    }

    public static float BetaflightRateCalc(float rcCommand, float rcRate, float superRate, float expo)
    {
        float absRcCommand = Mathf.Abs(rcCommand);

        if (rcRate > 2.0f)
            rcRate = rcRate + (14.54f * (rcRate - 2.0f));

        if (expo != 0)
            rcCommand = rcCommand * Mathf.Pow(absRcCommand, 3f) * expo + rcCommand * (1.0f - expo);

        float angleRate = 200.0f * rcRate * rcCommand;
        if (superRate != 0)
        {
            float rcSuperFactor = 1.0f / Constrainf(1.0f - (absRcCommand * superRate), 0.01f, 1.00f);
            angleRate *= rcSuperFactor;
        }

        return angleRate;
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

        rates = new List<RatesHandler>() 
        {
            new("roll", 1f, 0.7f, 0f),
            new("pith", 1f, 0.7f, 0f),
            new("yaw", 1f, 0.7f, 0f)
        };
    }


    private void Update()
    {
        
    }
}
