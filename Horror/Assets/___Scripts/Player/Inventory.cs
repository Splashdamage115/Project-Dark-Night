using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryHolder holder;
    public List<InventoryItem> Items;
    public int maxSize;

    public bool PickUp(InventoryItem item)
    {
        if (Items.Count >= maxSize) return false;

        Items.Add(item);

        holder.addItem();

        return true;
    }

    public bool checkItem(string name)
    {
        foreach(var i in Items)
        {
            if(i.ItemName == name) return true;
        }
        return false;
    }
    public bool checkItem(InventoryItem item)
    {
        foreach (var i in Items)
        {
            if (i.ItemName == item.ItemName) return true;
        }
        return false;
    }
}
