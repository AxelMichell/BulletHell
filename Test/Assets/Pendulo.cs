using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulo
{
    public float Armonic(float amplitude, float time)
    {
        float angle = time * Mathf.Deg2Rad;
        float pos = amplitude * Mathf.Cos(angle);

        return pos;
    } 
}
