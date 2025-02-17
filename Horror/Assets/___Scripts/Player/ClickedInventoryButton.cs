using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedInventoryButton : MonoBehaviour
{
    public int selfNum = 0;
    public void clickedThis()
    {
        GetComponentInParent<InventoryHolder>().setTexts(selfNum);
    }
}
