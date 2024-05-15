using UnityEngine;

public class RewardReciever : MonoBehaviour
{
    private Inventory inventory;
    private PlayerStats playerStats;
    private PlayerLvl playerLvl;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        playerStats = PlayerStats.instance;
        playerLvl = playerStats.gameObject.GetComponent<PlayerLvl>();
    }
    public void RecieveReward(RewardScriptable reward)
    {
        if (!reward.IsInventoryItem)
        {
            switch(reward.Name)
            {
                case "XP":
                    {
                        playerLvl.AddXp(reward.Quantity);
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
