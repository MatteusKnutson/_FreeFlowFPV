using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Input management and calculations to enable interactions between drone controller and simulation

    string controllerName;

    public static List<AxisHandler> inputChannels;
    public static List<RatesHandler> rates;

    public static string defaultAxisName = "Axis";

    Vector3 startPos;
    public Transform drone;

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

        startPos = drone.position;
    }


    private void Update()
    {
        if (GetConvertedInput(GetInputValue("Axis", inputChannels[4].axisIndex), inputChannels[4]) > 0.5)
        {
            drone.position = startPos;
            drone.rotation = Quaternion.Euler(0f, 0f, 0f);
            drone.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (GetConvertedInput(GetInputValue("Axis", inputChannels[5].axisIndex), inputChannels[5]) > 0.5)
        {
            drone.rotation = Quaternion.Euler(0f, drone.eulerAngles.y, 0f);
            drone.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}
