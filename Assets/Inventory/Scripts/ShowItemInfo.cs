using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfo : MonoBehaviour
{
    ItemHUDSlots itemsSlots;
    InventorySlot InventorySlot;
    TextMeshProUGUI Description;
    TextMeshProUGUI Name;
    TextMeshProUGUI Quantity;
    Image Image;
    public int itemQuantity;
    Button equipButton;
    bool _isEquiped;
    bool _isEquipeable;
    ItemUseable ItemUseable;
    SwordHolder _swordHolder;
    private void Start()
    {
        InventorySlot = GetComponent<InventorySlot>();
        _isEquipeable = InventorySlot.IsEquipeable;
        ItemUseable = InventorySlot.ItemUseable;
        Name = GameObject.FindGameObjectWithTag("Inventory/ItemName").GetComponent<TextMeshProUGUI>();
        Description = GameObject.FindGameObjectWithTag("Inventory/ItemDescription").GetComponent<TextMeshProUGUI>();
        Quantity = GameObject.FindGameObjectWithTag("Inventory/ItemQuantity").GetComponent<TextMeshProUGUI>();
        Image = GameObject.FindGameObjectWithTag("Inventory/ItemImage").GetComponent<Image>();
        equipButton = GameObject.FindGameObjectWithTag("Inventory/ItemEquipButton").GetComponent<Button>();
        itemsSlots = GameObject.FindGameObjectWithTag("HUD/EquipedItems").GetComponent<ItemHUDSlots>();
        _swordHolder = FindObjectOfType<SwordHolder>();
    }
    public void ShowInfo()
    {
        Name.text = InventorySlot.Name;
        Description.text = InventorySlot.Description;
        Quantity.text = InventorySlot.Quantity.ToString();
        Image.sprite = InventorySlot.ItemSprite;
        if (_isEquiped != true && _isEquipeable != false)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(EquipItem);
            _isEquiped = false;
        }
        else if (_isEquiped != false && _isEquipeable != false)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(UnEquipItem);
            _isEquiped = true;
        }
        equipButton.transform.DOScale(1, 0.7f);
    }

    void EquipItem()
    {
        if (InventorySlot.IsSword != true)
            itemsSlots.EquipItem(InventorySlot.ItemSprite, InventorySlot.Name, ItemUseable);
        else
            _swordHolder.EquipSword(InventorySlot.ItemObj);

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(UnEquipItem);
        _isEquiped = true;
    }
    void UnEquipItem()
    {
        if (InventorySlot.IsSword != true)
            itemsSlots.UnEquipItem(InventorySlot.Name);
        else
            _swordHolder.UnequipSword(InventorySlot.ItemObj);

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(EquipItem);
        _isEquiped = false;
    }
}
