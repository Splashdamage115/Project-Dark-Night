using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PickupText : MonoBehaviour
{
    public TextItem textItem;

    public TextClose pageHud;

    void Interact(Animator armsAnimator)
    {
        armsAnimator.SetBool("InteractPush", false);
        pageHud.gameObject.SetActive(true);
        pageHud.openPage(textItem);
    }
}
