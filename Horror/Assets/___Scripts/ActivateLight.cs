using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLight : MonoBehaviour
{
    public bool On = false;
    public bool emergencyLight = false;
    public GameObject[] lightObj;

    private void Start()
    {
        if (lightObj.Length <= 0 || lightObj == null) return;

        for (int i = 0; i < lightObj.Length; i++)
        {
            if (emergencyLight)
            {
                lightObj[i]?.SetActive(true);
                On = true;
            }
            else
            {
                lightObj[i]?.SetActive(false);
                On = false;
            }
        }
    }

    public void activate()
    {
        if (lightObj.Length <= 0 || lightObj == null) return;

        for (int i = 0; i < lightObj.Length; i++)
        {
            if (emergencyLight)
            {
                lightObj[i]?.SetActive(false);
                On = false;
            }
            else
            {
                lightObj[i]?.SetActive(true);
                On = true;
            }
        }
    }
}
