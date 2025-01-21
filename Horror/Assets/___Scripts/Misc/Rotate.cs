using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float maxRotate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitCircle * Random.Range(0.0f, maxRotate);
    }
}
