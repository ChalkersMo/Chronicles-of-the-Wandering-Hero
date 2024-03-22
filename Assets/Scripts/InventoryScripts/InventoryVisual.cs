using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisual : MonoBehaviour
{
    PlayerController _playerController;
    PlayerSwordAnimationController _playerSwordAnimationController;

    TextMeshProUGUI _nameItem;
    TextMeshProUGUI _descriptionItem;
    TextMeshProUGUI _quantityItem;
    Image _imageItem;
    [SerializeField] Sprite UIMaskSprite;

    Canvas _canvas;

    float _transitionSpeed;
    bool _isActive;
    private void Awake()
    {
        _nameItem = GameObject.FindGameObjectWithTag("Inventory/ItemName").GetComponent<TextMeshProUGUI>();
        _descriptionItem = GameObject.FindGameObjectWithTag("Inventory/ItemDescription").GetComponent<TextMeshProUGUI>();
        _quantityItem = GameObject.FindGameObjectWithTag("Inventory/ItemQuantity").GetComponent<TextMeshProUGUI>();
        _imageItem = GameObject.FindGameObjectWithTag("Inventory/ItemImage").GetComponent<Image>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerController = player.GetComponent<PlayerController>();
        _playerSwordAnimationController = player.GetComponent<PlayerSwordAnimationController>();
        _canvas = GetComponent<Canvas>();
        _transitionSpeed = 0;
        OffInventory();
        _transitionSpeed = 1;
        _isActive = false;
    }

    public void OnInventory()
    {
        _playerController.enabled = false;
        _playerSwordAnimationController.enabled = false;
        _canvas.sortingOrder = 5;
        transform.GetChild(0).DOScale(1, _transitionSpeed);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void OffInventory()
    {
        _playerController.enabled = true;
        _playerSwordAnimationController.enabled = true;
        _canvas.sortingOrder = 0;
        transform.GetChild(0).DOScale(0, _transitionSpeed);
        _nameItem.text = "Item name";
        _descriptionItem.text = "Here will be your item description";
        _quantityItem.text = "Item quantity";
        _imageItem.sprite = UIMaskSprite;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && _isActive != true)
        {
            _isActive = true;
            OnInventory();
        }
        else if(Input.GetKeyDown(KeyCode.B) && _isActive != false)
        {
            _isActive = false;
            OffInventory();
        }
    }
}
