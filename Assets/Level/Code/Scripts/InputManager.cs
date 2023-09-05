using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    string controllerName;
    List<AxisHandler> inputAxis = new List<AxisHandler>();

    void AddInputType(int axisIndex, string controlledAxis)
    {
        AxisHandler inputType = new AxisHandler(controlledAxis, axisIndex, 0, 1000);
        inputAxis.Add(inputType);
    }

    private void Start()
    {
        AddInputType(1, "throttle");
    }
}
