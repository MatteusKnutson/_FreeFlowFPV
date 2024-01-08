using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatesHandler
{
    // Stores rate settings for one channel.

    public string controlledChannel { get; set; }
    public float rcRate {  get; set; }
    public float sRate { get; set; }
    public float expo { get; set; }

    public RatesHandler (string controlledChannel, float rcRate, float sRate, float expo)
    {
        this.controlledChannel = controlledChannel;
        this.rcRate = rcRate;
        this.sRate = sRate;
        this.expo = expo;
    }
}
