using Cinemachine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField] private Sprite UIMaskSprite;

    [HideInInspector] public Transform panelInventory;

    [HideInInspector] public bool _isActive = false;

    private PlayerController _playerController;
    private CinemachineVirtualCamera ThirdPersonCamera;
    private UiAudio uiAudio;

    private TextMeshProUGUI _nameItem;
    private TextMeshProUGUI _descriptionItem;
    private TextMeshProUGUI _quantityItem;

    private Image _imageItem;  

    private Canvas _canvas;

    private float _transitionSpeed;

    private void Start()
    {
        _nameItem = GameObject.FindGameObjectWithTag("Inventory/ItemName").GetComponent<TextMeshProUGUI>();
        _descriptionItem = GameObject.FindGameObjectWithTag("Inventory/ItemDescription").GetComponent<TextMeshProUGUI>();
        _quantityItem = GameObject.FindGameObjectWithTag("Inventory/ItemQuantity").GetComponent<TextMeshProUGUI>();
        _imageItem = GameObject.FindGameObjectWithTag("Inventory/ItemImage").GetComponent<Image>();
        ThirdPersonCamera = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CinemachineVirtualCamera>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerController = player.GetComponent<PlayerController>();

        uiAudio = FindObjectOfType<UiAudio>();  

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
        _isActive = true;

        uiAudio.PlayOnSound();
    }
    public void OffInventory()
    {
        _playerController.enabled = true;
        panelInventory.DOScale(0, _transitionSpeed);
        _nameItem.text = "Item name";
        _descriptionItem.text = "Here will be your item description";
        _quantityItem.text = "Item quantity";
        _imageItem.sprite = UIMaskSprite;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ThirdPersonCamera.enabled = true;
        _isActive = false;

        uiAudio.PlayOffSound();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!_isActive)
                OnInventory();
            else
                OffInventory();
        }
    }
}
