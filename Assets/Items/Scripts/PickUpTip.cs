using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventorySlot))]
public class PickUpTip : MonoBehaviour
{
    GameObject _tip;
    GameObject _tempTip;
    GameObject _pressCanvas;
    Transform _tipSpawnPoint;

    InventorySlot _inventorySlot;
    private void Start()
    {
        _inventorySlot = GetComponent<InventorySlot>();
        _tip = _inventorySlot.TipToPickUp;
        _tip.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _inventorySlot.ItemSprite;
        _tip.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = _inventorySlot.Name;
        _pressCanvas = GameObject.Find("PressCanvas");
        _tipSpawnPoint = _pressCanvas.GetComponentInChildren<VerticalLayoutGroup>().transform;
    }

    public void InstantiateTip()
    {
        _tempTip = Instantiate(_tip, _tipSpawnPoint);
    }
    public void DestroyTip()
    {
        Destroy(_tempTip);
    }
}
