using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfo : MonoBehaviour
{
    ItemHUDSlots equipedItems;
    InventorySlot InventorySlot;
    TextMeshProUGUI Description;
    TextMeshProUGUI Name;
    TextMeshProUGUI Quantity;
    Image Image;
    public int itemQuantity;
    Button equipButton;
    bool _isEquiped;

    private void Start()
    {
        InventorySlot = GetComponent<InventorySlot>();
        Name = GameObject.FindGameObjectWithTag("Inventory/ItemName").GetComponent<TextMeshProUGUI>();
        Description = GameObject.FindGameObjectWithTag("Inventory/ItemDescription").GetComponent<TextMeshProUGUI>();
        Quantity = GameObject.FindGameObjectWithTag("Inventory/ItemQuantity").GetComponent<TextMeshProUGUI>();
        Image = GameObject.FindGameObjectWithTag("Inventory/ItemImage").GetComponent<Image>();
        equipButton = GameObject.FindGameObjectWithTag("Inventory/ItemEquipButton").GetComponent<Button>();
        equipedItems = GameObject.FindGameObjectWithTag("HUD/EquipedItems").GetComponent<ItemHUDSlots>();
        
    }
    public void ShowInfo()
    {
        Name.text = InventorySlot.Name;
        Description.text = InventorySlot.Description;
        Quantity.text = InventorySlot.Quantity.ToString();
        Image.sprite = InventorySlot.ItemSprite;
        if (_isEquiped != true)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(EquipItem);
            _isEquiped = false;
        }
        else
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(UnEquipItem);
            _isEquiped = true;
        }
        equipButton.transform.DOScale(1, 0.7f);
    }

    void EquipItem()
    {
        equipedItems.EquipItem(InventorySlot.ItemSprite, InventorySlot.Name);
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(UnEquipItem);
        _isEquiped = true;
    }
    void UnEquipItem()
    {
        equipedItems.UnEquipItem(InventorySlot.ItemSprite, InventorySlot.Name);
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(EquipItem);
        _isEquiped = false;
    }
}
