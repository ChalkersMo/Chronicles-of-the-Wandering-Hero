using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquiping : MonoBehaviour
{
    ItemHUDSlots itemsSlots;
    InventorySlot _currentInventorySlot;

    Button equipButton;

    ItemUseable _currentItemUseable;
    SwordHolder _swordHolder;
    private void Start()
    {
        equipButton = GameObject.FindGameObjectWithTag("Inventory/ItemEquipButton").GetComponent<Button>();
        itemsSlots = GameObject.FindGameObjectWithTag("HUD/EquipedItems").GetComponent<ItemHUDSlots>();
        _swordHolder = FindObjectOfType<SwordHolder>();
    }
    public void AddListenerToButton(InventorySlot inventorySlot)
    {
        _currentInventorySlot = inventorySlot;
        _currentItemUseable = inventorySlot.ItemUseable;
        if (!_currentInventorySlot.IsEquiped)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(EquipItem);
        }
        else if (_currentInventorySlot.IsEquiped)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(UnEquipItem);
        }
    }

    void EquipItem()
    {
        if (!_currentInventorySlot.IsSword)
            itemsSlots.EquipItem(_currentInventorySlot.ItemSprite, _currentInventorySlot.Name, _currentItemUseable);
        else
            _swordHolder.EquipSword(_currentInventorySlot.ItemObj);

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(UnEquipItem);
        _currentInventorySlot.IsEquiped = true;
    }
    void UnEquipItem()
    {
        if (!_currentInventorySlot.IsSword)
            itemsSlots.UnEquipItem(_currentInventorySlot.Name);
        else
            _swordHolder.UnequipSword(_currentInventorySlot.ItemObj);  
        
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(EquipItem);
        _currentInventorySlot.IsEquiped = false;
    }
}
