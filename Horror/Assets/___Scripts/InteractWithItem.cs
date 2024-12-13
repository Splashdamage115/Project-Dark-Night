using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField]
    private float interactDist = 3.0f;

    private GameObject highlighted;

    public GameObject normalReticle;
    public GameObject interactibleReticle;

    // Start is called before the first frame update
    void Start()
    {
        normalReticle.SetActive(true);
        interactibleReticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        Ray camRay = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
        RaycastHit hitInfo;
        //Debug.DrawRay(camRay.origin, camRay.direction * interactDist, Color.red);

        if(Physics.Raycast(camRay, out hitInfo))
        {
            if (hitInfo.distance < interactDist)
            {
                if (hitInfo.collider.gameObject.CompareTag("Interactible"))
                {
                    normalReticle.SetActive(false);
                    interactibleReticle.SetActive(true);
                    highlighted = hitInfo.collider.gameObject;
                }
                else
                {
                    normalReticle.SetActive(true);
                    interactibleReticle.SetActive(false);
                    highlighted = null;
                }
            }
        }
    }

    void Interact()
    {
        highlighted?.SendMessage("Interact");
    }
}
