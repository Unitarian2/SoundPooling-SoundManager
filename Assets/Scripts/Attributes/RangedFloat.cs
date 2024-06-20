using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RangedFloat
{
    public float MinValue;
    public float MaxValue;

    public RangedFloat(float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
