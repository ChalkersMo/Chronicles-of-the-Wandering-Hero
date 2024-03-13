using DG.Tweening;
using UnityEngine;

public class InventoryVisual : MonoBehaviour
{
    PlayerController _playerController;
    PlayerSwordAnimationController _playerSwordAnimationController;

    Canvas _canvas;

    float _transitionSpeed;
    bool _isActive;
    private void Awake()
    {
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
