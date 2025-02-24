using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetSpin : MonoBehaviour
{
    [SerializeField]
    private bool spinAxisX = false, spinAxisY = false, spinAxisZ = false;
    [SerializeField]
    private float spinSpeed = 1.0f;

    private void Start()
    {
        
        GetComponent<Rigidbody>().angularVelocity = transform.TransformDirection(
            spinAxisX ? spinSpeed : 0.0f,
            spinAxisY ? spinSpeed : 0.0f,
            spinAxisZ ? spinSpeed : 0.0f
            );

    }
}
