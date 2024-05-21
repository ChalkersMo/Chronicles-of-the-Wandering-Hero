using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Dictionary<string, InventorySlot> items = new Dictionary<string, InventorySlot>();
    public int maxSlots;
    GameObject currentItem;

    public bool AddItem(InventorySlot item, int quantity)
    {
        if (items.Count >= maxSlots)
        {
            return false;
        }

        if (items.ContainsKey(item.Name) && item.Stackable)
        {
            items[item.Name].Quantity += quantity;
            return true;
        }

        currentItem = Instantiate(item.ObjForInventory);
        InventorySlot itemInvSlot = currentItem.AddComponent<InventorySlot>();
        if(item.ItemUseable != null)
        {
            ItemUseable itemUseable = item.ItemUseable;
            itemUseable.Assign(currentItem);
        }       
        itemInvSlot.Assign(item);
        itemInvSlot.ItemUseable = currentItem.GetComponent<ItemUseable>();
        TryAddNonStackableItem(itemInvSlot);
        quantity--;
        currentItem.transform.SetParent(transform.GetComponentInChildren<GridLayoutGroup>().transform);
        currentItem.transform.localScale = new Vector3(1, 1, 1);
        currentItem.GetComponent<Image>().sprite = item.ItemSprite;

        if (items.ContainsKey(item.Name) && item.Stackable)
            items[item.Name].Quantity += quantity;

        return true;
    }

    private void TryAddNonStackableItem(InventorySlot itemInvSlot)
    {
        try
        {
            items.Add(itemInvSlot.Name, itemInvSlot);
        }
        catch
        {
            itemInvSlot.Name += Random.Range(0, 10000);
            try
            {
                items.Add(itemInvSlot.Name, itemInvSlot);
            }
            catch
            {
                TryAddNonStackableItem(itemInvSlot);
            }
        }
    }

    public bool DeleteItem(InventorySlot item, int quantity)
    {
        if (items.ContainsKey(item.Name) && item.Stackable)
        {
            item.Quantity -= quantity;
            if(item.Quantity <= 0)
                items.Remove(item.Name);

            return true;
        }
       
        items.Remove(item.Name);
        return true;
    }
}