using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfo : MonoBehaviour
{
    [SerializeField] ItemScriptable itemScriptable;
    TextMeshProUGUI Description;
    TextMeshProUGUI Name;
    Image Image;
    private void Start()
    {
        Name = GameObject.FindGameObjectWithTag("Inventory/ItemName").GetComponent<TextMeshProUGUI>();
        Description = GameObject.FindGameObjectWithTag("Inventory/ItemDescription").GetComponent<TextMeshProUGUI>();
        Image = GameObject.FindGameObjectWithTag("Inventory/ItemImage").GetComponent<Image>();
    }
    public void ShowInfo()
    {
        Name.text = itemScriptable.Name;
        Description.text = itemScriptable.Description;
        Image.sprite = itemScriptable.Sprite;
    }
}
