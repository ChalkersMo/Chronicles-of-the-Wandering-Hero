using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUDSlots : MonoBehaviour
{
    [SerializeField] GameObject[] Slots;
    readonly ItemUseable[] items = new ItemUseable[3];
    readonly Image[] SlotsImages = new Image[3];
    readonly Image[] SlotsRecoveringImages = new Image[3];
    [SerializeField] Sprite NullImage;
    [SerializeField] bool[] IsBusy;
    readonly string[] itemNames = new string[3];

    private void Start()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotsImages[i] = Slots[i].GetComponent<Image>();
            SlotsRecoveringImages[i] = Slots[i].transform.GetChild(0).GetComponent<Image>();
        }
    }

    public void EquipItem(Sprite sprite, string Name, ItemUseable itemUseable)
    {
        for(int i = 0; i < IsBusy.Length; i++)
        {
            if (IsBusy[i] != true)
            {
                itemNames[i] = Name;
                SlotsImages[i].sprite = sprite;
                items[i] = itemUseable;
                IsBusy[i] = true;
                break;
            }
        }
    }

    public void UnEquipItem(string Name)
    {
        for (int i = 0; i < IsBusy.Length; i++)
        {
            if (itemNames[i] == Name)
            {
                SlotsImages[i].sprite = NullImage;
                items[i] = null;
                itemNames[i] = null;
                IsBusy[i] = false;
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsBusy[0] != false && items[0]._isRecovering != true)
        { 
            items[0].UseItem();
            Recover1();
        }
        else if (Input.GetKeyDown(KeyCode.E) && IsBusy[1] != false && items[1]._isRecovering != true)
        {
            items[1].UseItem();
            Recover2();
        }
        else if (Input.GetKeyDown(KeyCode.R) && IsBusy[2] != false && items[2]._isRecovering != true)
        {
            items[2].UseItem();
            Recover3();
        }
    }

    void Recover1()
    {
        SlotsRecoveringImages[0].fillAmount = 1;
        SlotsRecoveringImages[0].DOFillAmount(0, items[0]._recoveringTime);
    }
    void Recover2()
    {
        SlotsRecoveringImages[1].fillAmount = 1;
        SlotsRecoveringImages[0].DOFillAmount(0, items[1]._recoveringTime);
    }
    void Recover3()
    {
        SlotsRecoveringImages[2].fillAmount = 1;
        SlotsRecoveringImages[0].DOFillAmount(0, items[2]._recoveringTime);
    }
}
