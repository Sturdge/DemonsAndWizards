using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void GetPercentage(this ref float x, float numerator, float denominator)
    {
        x = numerator / denominator;
    }
}
