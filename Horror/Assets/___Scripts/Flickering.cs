using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
    public GameObject[] lights;

    public float longestOff = 1.0f;
    public float shortestOff = 0.1f;

    public float longestOn = 1.0f;
    public float shortestOn = 0.1f;

    private bool on = true;

    public float currentFlip = 0.0f;

    private ActivateLight ActivateLightClass;

    private void Start()
    {
        currentFlip = Random.Range(shortestOn, longestOn);
        ActivateLightClass = GetComponent<ActivateLight>();
    }

    private void Update()
    {
        if (ActivateLightClass != null)
        {
            if (!ActivateLightClass.On)
                return;
        }

        currentFlip -= Time.deltaTime;
        if (currentFlip <= 0.0f)
        {
            if(on)
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(false);
                }
                currentFlip = Random.Range(shortestOff, longestOff);
                on = false;
            }
            else
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(true);
                }
                currentFlip = Random.Range(shortestOn, longestOn);
                on = true;
            }
        }
    }
}
