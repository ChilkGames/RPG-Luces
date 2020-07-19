using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseItem> inventory;

    public void RemoveItem()
    {
        if (inventory.Count > 0)
            inventory.RemoveAt(inventory.Count - 1);
    }
}
