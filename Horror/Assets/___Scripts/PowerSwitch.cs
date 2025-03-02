using System.Collections;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public float currentRotation = 0.0f;
    public float targetRotation = 90.0f;
    public float switchSpeed = 2.0f; // Speed of the switch rotation

    public GameObject SwitchItem;
    private bool isSwitching = false;

    bool switched = false;

    public void Interact()
    {
        if (!isSwitching)
            StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        isSwitching = true;
        float elapsedTime = 0f;
        float startRotation = currentRotation;
        float endRotation = (currentRotation == 0.0f) ? targetRotation : 0.0f; // Toggle between 0 and target
        currentRotation = endRotation; // Set new rotation value

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * switchSpeed;
            float t = Mathf.SmoothStep(0, 1, elapsedTime); // Smooth interpolation
            SwitchItem.transform.localRotation = Quaternion.Lerp(
                Quaternion.Euler(0f, 0f, startRotation),
                Quaternion.Euler(0f, 0f, endRotation),
                t
            );
            yield return null;
        }
        switched = !switched;
        if (switched)
        {
            activateLights();
        }
        
        isSwitching = false;
    }

    void activateLights()
    {
        ActivateLight[] t = FindObjectsOfType<ActivateLight>();
        foreach (ActivateLight a in t)
            a.activate();
    }
}
