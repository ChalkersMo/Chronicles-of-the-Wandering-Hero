using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public ScriptableObject item;
    public string Name;
    public string Description;
    public Sprite rarenessSprite;
    public GameObject itemObj;
    public int quantity;
    public bool stackable;
}
