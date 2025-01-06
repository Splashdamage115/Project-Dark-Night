using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 0;
    private void Start()
    {
        Destroy(gameObject, waitTime);
    }
}
