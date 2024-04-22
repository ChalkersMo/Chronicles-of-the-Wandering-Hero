using DG.Tweening;
using UnityEngine;

public class OffAllUI : MonoBehaviour
{
    public static OffAllUI Instance;

    private QuestVisual questVisual;
    private InventoryVisual inventoryVisual;
    private DialogueManager dialogueManager;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        questVisual = FindObjectOfType<QuestVisual>();
        inventoryVisual = FindObjectOfType<InventoryVisual>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    public void OffUI()
    {
        questVisual.OffQuestsPanel();

        inventoryVisual.OffInventory();

        dialogueManager.EndDialogue();
    }
}
