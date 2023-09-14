using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisHandler
{
    public string controlledChannel { get; set; }
    public int axisIndex { get; set; }
    public float axualMinValue { get; set; }
    public float axualMaxValue { get; set; }

    public AxisHandler(string controlledChannel, int axisIndex, int axualMinValue, int axualMaxValue)
    {
        this.controlledChannel = controlledChannel;
        this.axisIndex = axisIndex;
        this.axualMinValue = axualMinValue;
        this.axualMaxValue = axualMaxValue;
    }

}
