using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpZone : MonoBehaviour
{
    InventorySlot inventorySlot;
    PickUpTip _tip;
    Inventory inventory;
    ItemScriptable item;

    GameObject _pressCanvas;
    Transform _tipSpawnPoint;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventorySlot = GetComponent<InventorySlot>();
        _pressCanvas = GameObject.Find("PressCanvas");
        _tipSpawnPoint = _pressCanvas.GetComponentInChildren<VerticalLayoutGroup>().transform;
        _tip = GetComponent<PickUpTip>();
        item = inventorySlot.item;
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
                inventory.AddItem(inventorySlot, item.Quantity);
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
