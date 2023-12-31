using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePhysicsHandler : MonoBehaviour
{
    // Script to calculate and add the throttle/normal force to the drone taking into account all drone parameters

    public static Rigidbody r;
    public static DroneHandler droneHandler;

    void Start()
    {
        r = GetComponent<Rigidbody>();

        droneHandler = new DroneHandler(0.7f, 2300, 4, 2, 5, 25); //Default settings on startup
    }

    void FixedUpdate()
    {
        float controllerThrottleInput = (1 + InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, InputManager.inputChannels[0].axisIndex), InputManager.inputChannels[0])) / 2;

        float suppliedVoltage = controllerThrottleInput * droneHandler.batteryCells * 4.2f;

        float motorRPM = GetMotorRPM(droneHandler.motorKv, suppliedVoltage);

        float appliedForce = GetAppliedForce(motorRPM, droneHandler.propDiameter, droneHandler.propPitch, r.velocity.magnitude);

        r.AddRelativeForce(new Vector3(0, appliedForce * 4, 0), ForceMode.Force);

    }

    public float GetMotorRPM(float motorKv, float suppliedVoltage)
    {
        return motorKv * suppliedVoltage;
    }

    public float GetAppliedForce(float MotorRPM, float propDiameter, float propPitch, float airspeed)
    {
        // Calculates the supposed force one propeller can generate (ref. gyarte)

        return 4.392399f * Mathf.Pow(10, -8) * MotorRPM * (Mathf.Pow(propDiameter, 3.5f) / Mathf.Sqrt(propPitch) * (4.23333f * Mathf.Pow(10, -4) * MotorRPM * propPitch - airspeed));
    }

}
