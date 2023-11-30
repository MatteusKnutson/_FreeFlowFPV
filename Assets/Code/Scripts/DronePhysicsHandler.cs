using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePhysicsHandler : MonoBehaviour
{
    Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float controllerThrottleInput = (1 + InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, InputManager.inputChannels[0].axisIndex), InputManager.inputChannels[0])) / 2;
        Vector3 v3x = new Vector3(0, Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.x), Mathf.Sin(Mathf.Deg2Rad * transform.localRotation.x));

        Vector3 appliedForce = new Vector3(0, controllerThrottleInput * 9.82f * 4, 0);

        Debug.Log(appliedForce + " " + controllerThrottleInput);
        r.AddRelativeForce(appliedForce);
    }
}
