using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, ScriptableObject> items = new Dictionary<string, ScriptableObject>();
    public int maxSlots;
    GameObject currentItem;

    public bool AddItem(InventorySlot item, int quantity)
    {
        if (items.Count >= maxSlots)
        {
            return false;
        }

        if (items.ContainsKey(item.Name) && item.stackable)
        {
            item.quantity += quantity;
            return true;
        }
        items.Add(item.Name, item.itemScrObj);
        currentItem = Instantiate(item.ObjForInventory);
        currentItem.transform.SetParent(transform.GetComponentInChildren<GridLayoutGroup>().transform);
        currentItem.transform.localScale = new Vector3(1, 1, 1);
        return true;
    }
    public bool DeleteItem(InventorySlot item, int quantity)
    {
        if (items.ContainsKey(item.Name) && item.stackable)
        {
            item.quantity -= quantity;
            if(item.quantity <= 0)
                items.Remove(item.Name);

            return true;
        }
       
        items.Remove(item.Name);
        return true;
    }
}