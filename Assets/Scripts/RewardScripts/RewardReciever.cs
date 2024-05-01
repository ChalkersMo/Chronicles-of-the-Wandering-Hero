using UnityEngine;

public class RewardReciever : MonoBehaviour
{
    private Inventory inventory;
    private PlayerStats playerStats;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        playerStats = PlayerStats.instance;
    }
    public void RecieveReward(RewardScriptable reward)
    {
        if (!reward.IsInventoryItem)
        {
            switch(reward.Name)
            {
                case "XP":
                    {
                        playerStats.XP += reward.Quantity;
                        return;
                    }
                case "Gold":
                    {
                        return;
                    }
                default:
                    return;
            }
        }
        else
        {
            switch (reward.Name) 
            {
                case "HealingPod":
                    {
                        inventory.AddItem(reward.InvSlot, reward.Quantity);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }

        }
    }
    
}
