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
    Image _mainSwordOutlineImage;
    Image _secondaryOutlineSwordImage;

    Transform _mainSwordOutlineTransform;
    Transform _secondaryOutlineSwordTransform;
    [SerializeField] Sprite NullSprite;

    PlayerSwordController _playerMainSwordController;
    PlayerSwordController _playerSecondarySwordController;

    PlayerSwordAnimationController _playerSwordAnimationController;
    PlayerController _playerController;

    private void Start()
    {
        _mainSwordImage = GameObject.FindGameObjectWithTag("HUD/MainSwordImage").GetComponent<Image>();
        _secondarySwordImage = GameObject.FindGameObjectWithTag("HUD/SecondarySwordImage").GetComponent<Image>();
        _mainSwordOutlineTransform = _mainSwordImage.transform.GetChild(0);
        _secondaryOutlineSwordTransform = _secondarySwordImage.transform.GetChild(0);
        _mainSwordOutlineImage = _mainSwordOutlineTransform.GetComponent<Image>();
        _secondaryOutlineSwordImage = _secondaryOutlineSwordTransform.GetComponent<Image>();   

        _mainSwordOutlineTransform.DOScale(0, 0);
        _secondaryOutlineSwordTransform.DOScale(0, 0);

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
                _playerController.SwordEquiping();
                _mainSwordOutlineTransform.DOScale(2, 0);
                _mainSwordOutlineTransform.DOScale(1, 0.5f);
            }
            else
            {
                secondarySword = sword;
                _secondarySwordImage.sprite = controller.swordItem.Sprite;
                Instantiate(sword, _secondarySwordHolder);
                _isSecondarySwordEquiped = true;
                _playerSecondarySwordController = controller;
                _playerSwordAnimationController.AssignSwordController(_playerSecondarySwordController);
                _playerController.SwordEquiping();
                _secondaryOutlineSwordTransform.DOScale(2, 0);
                _secondaryOutlineSwordTransform.DOScale(1, 0.5f);
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

        _playerSwordAnimationController.AssignSwordController(null);
        _isMainSwordEquiped = false;
        _isSecondarySwordEquiped = false;

        _mainSwordOutlineTransform.DOScale(0, 0);
        _secondaryOutlineSwordTransform.DOScale(0, 0);
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
                _mainSwordOutlineTransform.DOScale(0, 0);
                _mainSwordOutlineImage.DOFade(1, 1);
                _mainSwordOutlineTransform.DOScale(1, 1);
                if (secondarySword != null)
                {
                    _secondarySwordHolder.GetChild(0).DOScale(0, 0);
                    _secondaryOutlineSwordTransform.DOScale(0, 1);
                    _secondaryOutlineSwordImage.DOFade(0, 0.8f);
                    _isSecondarySwordEquiped = false;
                }                  
            }
            else if(_isMainSwordEquiped != false)
            {
                _mainSwordHolder.GetChild(0).DOScale(0, 0.3f);
                _isMainSwordEquiped = false;
                _playerSwordAnimationController.AssignSwordController(null);
                _playerController.SwordDisEquiping();
                _mainSwordOutlineTransform.DOScale(2, 1);
                _mainSwordOutlineImage.DOFade(0, 1);
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
                _secondaryOutlineSwordTransform.DOScale(0, 0);
                _secondaryOutlineSwordImage.DOFade(1, 1);
                _secondaryOutlineSwordTransform.DOScale(1, 1);
                if (mainSword != null)
                {
                    _mainSwordHolder.GetChild(0).DOScale(0, 0);
                    _mainSwordOutlineTransform.DOScale(0, 1);
                    _mainSwordOutlineImage.DOFade(0, 0.8f);
                    _isMainSwordEquiped = false;
                }                   
            }
            else if (_isSecondarySwordEquiped != false)
            {
                _secondarySwordHolder.GetChild(0).DOScale(0, 0.3f);
                _isSecondarySwordEquiped = false;
                _playerSwordAnimationController.AssignSwordController(null);
                _playerController.SwordDisEquiping();
                _secondaryOutlineSwordTransform.DOScale(2, 1);
                _secondaryOutlineSwordImage.DOFade(0, 1);
            }
        }
    }
}
