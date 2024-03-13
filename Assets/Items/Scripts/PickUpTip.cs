using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventorySlot))]
public class PickUpTip : MonoBehaviour
{
    GameObject _tip;
    GameObject _tempTip;
    GameObject _pressCanvas;
    Transform _tipSpawnPoint;
    private void Start()
    {
        _tip = GetComponent<InventorySlot>().item.TipToPickUp;
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
