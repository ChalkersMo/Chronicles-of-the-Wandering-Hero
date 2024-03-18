using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUDSlots : MonoBehaviour
{
    [SerializeField] GameObject[] Slots;
    Image[] SlotsImages = new Image[3];
    [SerializeField] Sprite NullImage;
    [SerializeField] bool[] IsBusy;
    string[] itemNames = new string[3];

    private void Start()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            SlotsImages[i] = Slots[i].GetComponent<Image>();
        }
    }

    public void EquipItem(Sprite sprite, string Name)
    {
        for(int i = 0; i < IsBusy.Length; i++)
        {
            if (IsBusy[i] != true)
            {
                itemNames[i] = Name;
                SlotsImages[i].sprite = sprite;
                IsBusy[i] = true;
                break;
            }
        }
    }

    public void UnEquipItem(Sprite sprite, string Name)
    {
        for (int i = 0; i < IsBusy.Length; i++)
        {
            if (itemNames[i] == Name)
            {
                SlotsImages[i].sprite = NullImage;
                itemNames[i] = null;
                IsBusy[i] = false;
                break;
            }
        }
    }
}
