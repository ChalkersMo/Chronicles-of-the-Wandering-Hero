using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwordHolder : MonoBehaviour
{
    bool _isMainSwordEquiped;
    bool _isSecondarySwordEquiped;

    GameObject mainSword;
    GameObject secondarySword;

    Transform _mainSwordHolder;
    Transform _secondarySwordHolder;

    Image _mainSwordImage;
    Image _secondarySwordImage;
    [SerializeField] Sprite NullSprite;

    PlayerSwordController _playerMainSwordController;
    PlayerSwordController _playerSecondarySwordController;

    PlayerSwordAnimationController _playerSwordAnimationController;
    PlayerController _playerController;

    private void Start()
    {
        _mainSwordImage = GameObject.FindGameObjectWithTag("HUD/MainSwordImage").GetComponent<Image>();
        _secondarySwordImage = GameObject.FindGameObjectWithTag("HUD/SecondarySwordImage").GetComponent<Image>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerSwordAnimationController = player.GetComponent<PlayerSwordAnimationController>();
        _playerController = player.GetComponent<PlayerController>();

        _mainSwordHolder = transform.GetChild(0);
        _secondarySwordHolder = transform.GetChild(1);
    }
    public void EquipSword(GameObject sword)
    {
        UnequipSword(sword);
        UnequipAll();

        if (sword.TryGetComponent(out PlayerSwordController controller))
        {
            if(controller.swordItem.IsMain != false)
            {
                mainSword = sword;
                _mainSwordImage.sprite = controller.swordItem.Sprite;
                Instantiate(sword, _mainSwordHolder);
                _isMainSwordEquiped = true;
                _playerMainSwordController = controller;
                _playerSwordAnimationController.AssignSwordController(_playerMainSwordController);
            }
            else
            {
                secondarySword = sword;
                _secondarySwordImage.sprite = controller.swordItem.Sprite;
                Instantiate(sword, _secondarySwordHolder);
                _isSecondarySwordEquiped = true;
                _playerSecondarySwordController = controller;
                _playerSwordAnimationController.AssignSwordController(_playerSecondarySwordController);
            }
        }
        
    }
    public void UnequipSword(GameObject sword)
    {
        if(mainSword == sword) 
        {
            mainSword = null;
            _mainSwordImage.sprite = NullSprite;

            if(_mainSwordHolder.GetChild(0) != null)
                Destroy(_mainSwordHolder.GetChild(0).gameObject);

            _isMainSwordEquiped = false;
        }
        else if(secondarySword == sword)
        {
            secondarySword = null;
            _secondarySwordImage.sprite= NullSprite;

            if(_secondarySwordHolder.GetChild(1) != null)
                Destroy(_secondarySwordHolder.GetChild(1).gameObject);

            _isSecondarySwordEquiped = false;
        }
    }
    void UnequipAll()
    {
        if (mainSword != null)
            _mainSwordHolder.GetChild(0).DOScale(0, 0);
        if (secondarySword != null)
            _secondarySwordHolder.GetChild(0).DOScale(0, 0);

        _isMainSwordEquiped = false;
        _isSecondarySwordEquiped = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
           
            if (_isMainSwordEquiped != true)
            {
                _mainSwordHolder.GetChild(0).DOScale(1, 0.3f);
                _isMainSwordEquiped = true;
                _playerSwordAnimationController.AssignSwordController(_playerMainSwordController);
                _playerController.SwordEquiping();
                if (secondarySword != null)
                {
                    _secondarySwordHolder.GetChild(0).DOScale(0, 0);
                    _isSecondarySwordEquiped = false;
                }                  
            }
            else if(_isMainSwordEquiped != false)
            {
                _mainSwordHolder.GetChild(0).DOScale(0, 0.3f);
                _isMainSwordEquiped = false;
                _playerSwordAnimationController.AssignSwordController(null);
                _playerController.SwordDisEquiping();
            }

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_isSecondarySwordEquiped != true)
            {
                _secondarySwordHolder.GetChild(0).DOScale(1, 0.3f);
                _isSecondarySwordEquiped = true;
                _playerSwordAnimationController.AssignSwordController(_playerSecondarySwordController);
                _playerController.SwordEquiping();
                if (mainSword != null)
                {
                    _mainSwordHolder.GetChild(0).DOScale(0, 0);
                    _isMainSwordEquiped = false;
                }                   
            }
            else if (_isSecondarySwordEquiped != false)
            {
                _secondarySwordHolder.GetChild(0).DOScale(0, 0.3f);
                _isSecondarySwordEquiped = false;
                _playerSwordAnimationController.AssignSwordController(null);
                _playerController.SwordDisEquiping();
            }
        }
    }
}
