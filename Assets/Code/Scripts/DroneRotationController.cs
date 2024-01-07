using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneRotationController : MonoBehaviour
{
    public float angularSpeed;
    public Rigidbody r;

    float GetRotationSpeedDeg(AxisHandler inputChannel, int nrRatesList)
    {
        return InputManager.BetaflightRateCalc(
                InputManager.GetConvertedInput(
                    InputManager.GetInputValue(InputManager.defaultAxisName, inputChannel.axisIndex), inputChannel),
                        InputManager.rates[nrRatesList].rcRate, InputManager.rates[nrRatesList].sRate, InputManager.rates[nrRatesList].expo);
    }

    void RotateDrone(Vector3 rotationSpeedDPS)
    {
        transform.Rotate(rotationSpeedDPS * Time.deltaTime, Space.Self);
        r.rotation = transform.rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angularSpeed = r.angularVelocity.magnitude * Mathf.Rad2Deg;
        //r.angularVelocity = new Vector3(GetRotationSpeedDeg(InputManager.inputChannels[2]) * Mathf.Deg2Rad, GetRotationSpeedDeg(InputManager.inputChannels[1]) * Mathf.Deg2Rad, -1 * GetRotationSpeedDeg(InputManager.inputChannels[3]) * Mathf.Deg2Rad);

        Vector3 rotation = new Vector3(GetRotationSpeedDeg(InputManager.inputChannels[2], 1), GetRotationSpeedDeg(InputManager.inputChannels[1], 0), -GetRotationSpeedDeg(InputManager.inputChannels[3], 2));
        RotateDrone(rotation);


    }
}
