using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public ScriptableObject itemScrObj;
    public ItemScriptable item;
    public string Name;
    public string Description;
    public Sprite rarenessSprite;
    public GameObject itemObj;
    public GameObject ObjForInventory;
    public int quantity;
    public bool stackable;
}
