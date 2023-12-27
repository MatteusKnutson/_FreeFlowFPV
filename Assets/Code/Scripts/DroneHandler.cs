using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHandler
{
    public float weight { get; set; }
    public float motorKv { get; set; }
    public float batteryCells { get; set; }
    public float propPitch { get; set; }
    public float propDiameter { get; set; }

    public DroneHandler(float weight, float motorKv, float batteryCells, float propPitch, float propDiameter)
    {
        this.weight = weight;
        this.motorKv = motorKv;
        this.batteryCells = batteryCells;
        this.propPitch = propPitch;
        this.propDiameter = propDiameter;
    }
}
