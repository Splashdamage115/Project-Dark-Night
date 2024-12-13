using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField]
    private Transform PivotPoint;

    [SerializeField]
    private GameObject Door;

    bool open = false;
    private bool opening = false;
    private float targetRotation = 0.0f;
    private float currentRotation = 0.0f;
    private float interpolationSpeed = 3.0f; 
    private float interpolationProgress = 0.0f;

    void Interact()
    {
        if (Door != null && PivotPoint != null)
        {
            if (open)
            {
                targetRotation = 0.0f;
                currentRotation = 90.0f;
            }
            else
            {
                targetRotation = 90.0f;
                currentRotation = 0.0f;
            }
            open = !open;
            interpolationProgress = 0.0f;
            opening = true;
        }
        else
        {
            Debug.Log("DOOR OR PIVOT NOT APPLIED!");
        }
        Debug.Log("Opened Door");
    }

    private void Update()
    {
        if (opening)
        {
            interpolationProgress += interpolationSpeed * Time.deltaTime;
            Door.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(-90.0f, currentRotation, Door.transform.localEulerAngles.z),
                Quaternion.Euler(-90.0f, targetRotation, Door.transform.localEulerAngles.z),
                interpolationProgress);
        }
        if (open && Door.transform.localRotation.y >= targetRotation - 0.1f) opening = false;
        else if(!open && Door.transform.localRotation.y <= targetRotation - 0.1f) opening = false;
    }
}
