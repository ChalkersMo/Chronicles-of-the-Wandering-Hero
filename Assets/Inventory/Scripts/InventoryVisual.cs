using UnityEngine;

public class InventoryVisual : MonoBehaviour
{
    private void OnEnable()
    {
        OnInventory();
    }
    private void OnDisable()
    {
        OffInventory();
    }

    public void OnInventory()
    {
        GetComponent<Canvas>().sortingOrder = 5;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OffInventory()
    {
        GetComponent<Canvas>().sortingOrder = 0;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
