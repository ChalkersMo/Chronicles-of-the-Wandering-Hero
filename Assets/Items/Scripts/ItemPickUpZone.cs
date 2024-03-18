using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpZone : MonoBehaviour
{
    InventorySlot inventorySlot;
    PickUpTip _tip;
    Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventorySlot = GetComponent<InventorySlot>();
        _tip = GetComponent<PickUpTip>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            _tip.InstantiateTip();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _tip.DestroyTip();
                inventory.AddItem(inventorySlot, inventorySlot.Quantity);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tip.DestroyTip();
        }
    }
}
