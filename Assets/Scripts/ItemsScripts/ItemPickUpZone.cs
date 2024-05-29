using UnityEngine;

public class ItemPickUpZone : MonoBehaviour
{
    private InventorySlot inventorySlot;
    private PickUpTip _tip;
    private Inventory inventory;
    private PlayerAudio playerAudio;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        playerAudio = FindObjectOfType<PlayerAudio>();
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
                PickUp();
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

    private void PickUp()
    {
        playerAudio.PlayPickUpSound();
        _tip.DestroyTip();
        inventory.AddItem(inventorySlot, inventorySlot.Quantity);
        if (TryGetComponent(out QuestProgresser questProgresser))
        {
            questProgresser.ProgressQuest();
        }
        Destroy(gameObject);
    }
}
