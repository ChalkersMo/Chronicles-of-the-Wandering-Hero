using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InventorySlot))]
public class HealingPod : ItemUseable
{
    private PlayerStats playerStats;
    private PlayerDamageable playerDamageable;

    private InventorySlot invSlot;
    private ItemHUDSlots _slots;
    private Inventory inventory;

    private float _healPoints;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inventory = GetComponentInParent<Inventory>();
        invSlot = GetComponent<InventorySlot>();
        _slots = FindObjectOfType<ItemHUDSlots>();
        playerStats = player.GetComponent<PlayerStats>();
        playerDamageable = player.GetComponent<PlayerDamageable>();
        _healPoints = 20 * playerStats.HealingMultiply;
        _recoveringTime = 5;
    }

    public override void UseItem()
    {
        if(invSlot.Quantity > 0)
        {
            Heal();
            _isRecovering = true;
            inventory.DeleteItem(invSlot, 1);
            StartCoroutine(ItemRecovering());
        }
    }
    public override IEnumerator ItemRecovering()
    {
        yield return new WaitForSeconds(_recoveringTime);
         _isRecovering = false;

        if (invSlot.Quantity <= 0)
        {
            UnequipItem();
            Destroy(gameObject);
        }
        StopCoroutine(ItemRecovering());
    }
    public override void UnequipItem()
    {
        _slots.UnEquipItem(invSlot.Name);
    }
    public override void Assign(GameObject targetObj)
    {
        targetObj.AddComponent<HealingPod>();
    }

    void Heal()
    {
        playerDamageable.Heal(_healPoints);
    }
}
