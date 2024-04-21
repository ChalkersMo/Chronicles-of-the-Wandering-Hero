using Cinemachine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisual : MonoBehaviour
{
    private PlayerController _playerController;
    private CinemachineVirtualCamera ThirdPersonCamera;

    private TextMeshProUGUI _nameItem;
    private TextMeshProUGUI _descriptionItem;
    private TextMeshProUGUI _quantityItem;

    private Image _imageItem;

    [SerializeField] private Sprite UIMaskSprite;

    private Canvas _canvas;

    [HideInInspector] public Transform panelInventory;

    private float _transitionSpeed;

    [HideInInspector] public bool _isActive;

    private void Awake()
    {
        _nameItem = GameObject.FindGameObjectWithTag("Inventory/ItemName").GetComponent<TextMeshProUGUI>();
        _descriptionItem = GameObject.FindGameObjectWithTag("Inventory/ItemDescription").GetComponent<TextMeshProUGUI>();
        _quantityItem = GameObject.FindGameObjectWithTag("Inventory/ItemQuantity").GetComponent<TextMeshProUGUI>();
        _imageItem = GameObject.FindGameObjectWithTag("Inventory/ItemImage").GetComponent<Image>();
        ThirdPersonCamera = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CinemachineVirtualCamera>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerController = player.GetComponent<PlayerController>();

        _canvas = GetComponent<Canvas>();
        panelInventory = transform.GetChild(0);
        _transitionSpeed = 0;
        OffInventory();
        _transitionSpeed = 1;
        _isActive = false;
    }

    public void OnInventory()
    {
        OffAllUI.Instance.OffUI();
        CanvasesSortingOrder.Instance.ShowOnFirst(_canvas);

        _playerController.enabled = false;        
        panelInventory.DOScale(1, _transitionSpeed);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ThirdPersonCamera.enabled = false;
    }
    public void OffInventory()
    {
        _playerController.enabled = true;
        _canvas.sortingOrder = 0;
        panelInventory.DOScale(0, _transitionSpeed);
        _nameItem.text = "Item name";
        _descriptionItem.text = "Here will be your item description";
        _quantityItem.text = "Item quantity";
        _imageItem.sprite = UIMaskSprite;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ThirdPersonCamera.enabled = true;
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
