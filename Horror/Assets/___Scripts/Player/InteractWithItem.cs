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
    public TextMeshProUGUI hudText;

    private bool castRay = false;

    // Start is called before the first frame update
    void Start()
    {
        hudText.text = "";
        normalReticle.SetActive(true);
        interactibleReticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        Ray camRay = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
        RaycastHit hitInfo;
        castRay = false;

        if (Physics.Raycast(camRay, out hitInfo))
        {
            if (hitInfo.distance < interactDist)
            {
                if (hitInfo.collider.gameObject.CompareTag("Interactible"))
                {
                    normalReticle.SetActive(false);
                    interactibleReticle.SetActive(true);
                    highlighted = hitInfo.collider.gameObject;
                    if (highlighted != null)
                    {
                        hudText.text = highlighted.GetComponent<InteractText>().getText();
                        hudText.color = highlighted.GetComponent<InteractText>().Color;
                    }
                    castRay = true;
                }
            }
        }
        if(!castRay)
        {
            hudText.text = "";
            hudText.color = Color.white;
            normalReticle.SetActive(true);
            interactibleReticle.SetActive(false);
            highlighted = null;
        }
    }

    void Interact(Animator animator)
    {
        if (highlighted != null)
        {
            animator.SetBool("InteractValid", true);
            highlighted.SendMessage("Interact", animator);
        }
        else
        {
            // dont need to set bools to true as interact is invalid
        }
    }
}
