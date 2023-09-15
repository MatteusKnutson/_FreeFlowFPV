using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisHandler
{
    public string controlledChannel { get; set; }
    public int axisIndex { get; set; }
    public float actualMinValue { get; set; }
    public float actualMaxValue { get; set; }
    public bool isInverted { get; set; }

    public AxisHandler(string controlledChannel, int axisIndex, float axualMinValue, float axualMaxValue, bool isInverted)
    {
        this.controlledChannel = controlledChannel;
        this.axisIndex = axisIndex;
        this.actualMinValue = axualMinValue;
        this.actualMaxValue = axualMaxValue;
        this.isInverted = isInverted;
    }

}
