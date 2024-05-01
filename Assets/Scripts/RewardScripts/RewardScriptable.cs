using UnityEngine;

[CreateAssetMenu(fileName = "NewReward", menuName = "Reward")]
public class RewardScriptable : ScriptableObject
{
    public string Name;
    [Space]
    public bool IsInventoryItem;
    [Space]
    public int Quantity;
    [Space]
    public Sprite RewardSprite;
    [Space, Header("If it is invenventory item put here gameObject with InventorySlot script")]
    public InventorySlot InvSlot;
}
