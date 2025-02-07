using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MathLibrary : MonoBehaviour
{
    // use this for distance checks as they are faster
    public static float squareDistancebetweenPoints(Vector3 t_pos1, Vector3 t_pos2)
    {
        return ((t_pos2.x - t_pos1.x) * (t_pos2.x - t_pos1.x) + (t_pos2.y - t_pos1.y) * (t_pos2.y - t_pos1.y) + (t_pos2.z - t_pos1.z) * (t_pos2.z - t_pos1.z));
    }

    public static float DistancebetweenPoints(Vector3 t_pos1, Vector3 t_pos2)
    {
        return Mathf.Sqrt((t_pos2.x - t_pos1.x) * (t_pos2.x - t_pos1.x) + (t_pos2.y - t_pos1.y) * (t_pos2.y - t_pos1.y) + (t_pos2.z - t_pos1.z) * (t_pos2.z - t_pos1.z));
    }

    public static Vector3 displacement(Vector3 t_loaction, Vector3 t_aim)
    {
        Vector3 displacement = t_aim - t_loaction;
        displacement /= Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y + displacement.z * displacement.z);
        return displacement;
    }
    // convert a float degrees and return radian amount
    public static float degreesToRadians(float t_degrees)
    {
        float radians = t_degrees * (Mathf.PI / 180.0f);
        return radians;
    }

    // convert float radians and return degrees amount
    public static float radiansToDegrees(float t_radians)
    {
        float degrees = t_radians * 180.0f / Mathf.PI;
        return degrees;
    }

    // move vector converted to an angle
    public static float displacementToDegrees(Vector2 t_displacement)
    {
        float angle = radiansToDegrees(Mathf.Atan2(t_displacement.y, t_displacement.x));
        if (angle < 0.0f)
            angle = 360.0f + angle;
        if (angle > 360.0f)
        {
            angle = angle - 360.0f;
        }
        return angle;
    }

    // make a move angle into a displacement amount
    public static Vector2 angleToPosition(float t_hypLen, float t_angle)
    {
        Vector2 position;
        t_angle = degreesToRadians(t_angle);
        position.x = Mathf.Cos(t_angle) * t_hypLen;
        position.y = Mathf.Sin(t_angle) * t_hypLen;
        return position;
    }
}
