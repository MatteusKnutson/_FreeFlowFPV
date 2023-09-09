using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisHandler
{
    public string controlledAxis { get; set; }
    public int axisIndex { get; set; }
    public int axualMinValue { get; set; }
    public int axualMaxValue { get; set; }
    public float inputValue { get; set; }

    public AxisHandler(string controlledAxis, int axisIndex, int axualMinValue, int axualMaxValue)
    {
        this.controlledAxis = controlledAxis;
        this.axisIndex = axisIndex;
        this.axualMinValue = axualMinValue;
        this.axualMaxValue = axualMaxValue;
    }
}
