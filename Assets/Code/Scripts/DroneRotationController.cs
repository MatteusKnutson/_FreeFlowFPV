using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneRotationController : MonoBehaviour
{
    public Rigidbody r;

    float GetRotationSpeedDeg(AxisHandler inputChannel, int nrRatesList)
    {
        // returns the target rotation speed of the drone given the controlledChannel and inputChannel

        float rcCommand = InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, inputChannel.axisIndex), inputChannel);
        float rotationSpeed = BetaflightRateCalc(rcCommand, InputManager.rates[nrRatesList].rcRate, InputManager.rates[nrRatesList].sRate, InputManager.rates[nrRatesList].expo);

        return rotationSpeed;
    }

    void RotateDrone(Vector3 rotationSpeedDPS)
    {
        transform.Rotate(rotationSpeedDPS * Time.deltaTime, Space.Self);
        r.rotation = transform.rotation;
    }

    

    public static float BetaflightRateCalc(float rcCommand, float rcRate, float superRate, float expo)
    {
        // Calculates the target angleRates from the rates settings and rcCommand (ref. betaflight scource code)

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

    static float Constrainf(float amt, float low, float high)
    {
        if (amt < low)
            return low;
        else if (amt > high)
            return high;
        else
            return amt;
    }

    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Vector3 rotation = new Vector3(GetRotationSpeedDeg(InputManager.inputChannels[2], 1), GetRotationSpeedDeg(InputManager.inputChannels[1], 0), -GetRotationSpeedDeg(InputManager.inputChannels[3], 2));
        RotateDrone(rotation);

    }
}
