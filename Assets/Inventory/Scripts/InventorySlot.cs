using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite ItemSprite;
    public GameObject ItemObj;
    public GameObject ObjForInventory;
    public GameObject TipToPickUp;
    public int Quantity;
    public bool Stackable;
    public ItemUseable ItemUseable;

    public void Assign(InventorySlot source)
    {
        var type = typeof(InventorySlot);
        foreach (var field in type.GetFields())
        {         
            field.SetValue(this, field.GetValue(source));  
        }
    }
}