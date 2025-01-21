using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    [SerializeField]
    Vector3 direction = Vector3.zero;
    [SerializeField]
    float speed = 1.0f;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(direction * speed);
    }
}
