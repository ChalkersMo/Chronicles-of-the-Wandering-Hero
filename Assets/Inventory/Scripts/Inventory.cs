using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots;
    public int maxSlots;
    GameObject currentItem;
    InventoryVisual inventoryVisual;

    private void Start()
    {
        inventoryVisual = GetComponent<InventoryVisual>();
    }
    public bool AddItem(InventorySlot item, int quantity)
    {
        if (slots.Count >= maxSlots)
        {
            return false;
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item && slot.stackable)
            {
                slot.quantity += quantity;
                return true;
            }
        }

        inventoryVisual.enabled = true;
        slots.Add(item);
        currentItem = item.itemObj;
        currentItem.transform.SetParent(transform.GetComponentInChildren<GridLayoutGroup>().transform);
        currentItem.transform.localScale = new Vector3(1, 1, 1);
        inventoryVisual.enabled = false;
        return true;
    }
    public bool DeleteItem(InventorySlot item, int quantity)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item && slot.stackable)
            {
                slot.quantity -= quantity;
                return true;
            }
        }

        slots.Remove(item);
        return true;
    }
}