using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisHandler
{
    string controlledAxis { get; set; }
    int axisIndex { get; set; }
    int axualMinValue { get; set; }
    int axualMaxValue { get; set; }
    float inputValue { get; set; }

    public AxisHandler(string controlledAxis, int axisIndex, int axualMinValue, int axualMaxValue)
    {
        this.controlledAxis = controlledAxis;
        this.axisIndex = axisIndex;
        this.axualMinValue = axualMinValue;
        this.axualMaxValue = axualMaxValue;
    }
}
