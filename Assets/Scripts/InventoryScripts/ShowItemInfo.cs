using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI Quantity;

    [SerializeField] Image Image;

    [SerializeField] Button equipButton;

    ItemEquiping itemEquiping;
    void Start()
    {
        itemEquiping = FindObjectOfType<ItemEquiping>();
        if(itemEquiping != null)
            equipButton = itemEquiping.GetComponent<Button>();
    }

    public void ShowInfo(InventorySlot inventorySlot)
    {
        Name.text = inventorySlot.Name;
        Description.text = inventorySlot.Description;
        Quantity.text = inventorySlot.Quantity.ToString();
        Image.sprite = inventorySlot.ItemSprite;
        if (inventorySlot.IsEquipeable)
        {
            itemEquiping.AddListenerToButton(inventorySlot);
            equipButton.transform.DOScale(1, 0.7f);
        }
        else
        {
            equipButton.transform.DOScale(0, 0.7f);
        }       
    }


}
