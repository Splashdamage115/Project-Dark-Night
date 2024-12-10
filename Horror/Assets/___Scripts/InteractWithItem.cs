using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField]
    private float interactDist = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        Ray camRay = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
        Debug.DrawRay(camRay.origin, camRay.direction * interactDist, Color.red);
    }
}
