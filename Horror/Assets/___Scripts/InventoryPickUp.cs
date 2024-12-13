using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickUp : MonoBehaviour
{
    public InventoryItem Item;

    void Interact()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            if(go.GetComponent<Inventory>().PickUp(Item))
                Destroy(gameObject);
        }
    }
}
