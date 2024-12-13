using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MathLibrary : MonoBehaviour
{
    public static float interpolate(float current, float end)
    {
        return end - current;
    }
}
