using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemUseable ItemUseable;
    ShowItemInfo _showItemInfo;

    public GameObject ItemObj;
    public GameObject ObjForInventory;
    public GameObject TipToPickUp;

    public Sprite ItemSprite;

    public int Quantity;

    public bool Stackable;   
    public bool IsEquipeable;
    public bool IsEquiped;
    public bool IsSword;

    public string Name;
    public string Description;


    private void Start()
    {
        if (TryGetComponent(out Button showInfoButton))
        {
            showInfoButton.onClick.AddListener(ShowItemInfo);
            _showItemInfo = FindObjectOfType<ShowItemInfo>();
        }          
    }
    void ShowItemInfo()
    {
        _showItemInfo.ShowInfo(this);
    }
    public void Assign(InventorySlot source)
    {
        var type = typeof(InventorySlot);
        foreach (var field in type.GetFields())
        {         
            field.SetValue(this, field.GetValue(source));  
        }
    }
}