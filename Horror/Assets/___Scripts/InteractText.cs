using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    public string text;
    public Color Color = Color.white;

    void Start()
    {
        if (gameObject.GetComponent<InventoryPickUp>() != null)
        {
            text = "Pick Up ";
            text += gameObject.GetComponent<InventoryPickUp>().Item.DisplayName;
        }
    }

    public string getText()
    {
        return text;
    }
}
