using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InventorySlot))]
public class ItemUseable : MonoBehaviour
{
    public int _recoveringTime;
    public bool _isRecovering = false;

    public virtual void UseItem()
    {
        _isRecovering = true;
        StartCoroutine(ItemRecovering());
    }
    public virtual IEnumerator ItemRecovering()
    {
        yield return new WaitForSecondsRealtime(_recoveringTime);
        _isRecovering = false;
        StopCoroutine(ItemRecovering());
    }
    public virtual void UnequipItem()
    {

    }
    public virtual void Assign(GameObject targetObj)
    {
        targetObj.AddComponent<ItemUseable>();
    }
}
