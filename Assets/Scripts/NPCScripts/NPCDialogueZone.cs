using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(Collider))]
public class NPCDialogueZone : MonoBehaviour
{ 
    private Collider _collider;

    [SerializeField] private NPC NPCScriptableObject;
    [SerializeField] private GameObject PressButtonTip;
    [SerializeField] private GameObject NameTextGO;

    private TextMeshProUGUI _nameText;

    private DialogueManager dialogueManager;
    private PlayerController playerController;

    private bool isPlayerNear = false;

   private void Start()
    {
        InstantiateName();
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PressButtonTip != null)
            {
                PressButtonTip.SetActive(true);
                isPlayerNear = true;
            }
        }
    }
    private void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerNear)
        {
            StartingDialogue();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PressButtonTip != null)
            {
                PressButtonTip.SetActive(false);
                isPlayerNear = false;
            }
        }
    }
    private void StartingDialogue()
    {
        if (NPCScriptableObject.Quests.Count > 0)
            OnQuest();
        else
        {
            ForStartingDialogue();
            dialogueManager.StartDialogue(NPCScriptableObject.dialogues[0]);
        }

        if (PressButtonTip != null)
        {
            isPlayerNear = false;
            PressButtonTip.SetActive(false);
        }            
    }
    private void OnQuest()
    {
        bool isHaveAvaliableQuest = false;
        foreach (QuestScriptable quest in NPCScriptableObject.Quests)
        {
            if (!quest.IsCompleted && !quest.IsAccepted && PlayerStats.instance.CurrentLvl >= quest.LvlToStart)
            {
                ForStartingDialogue();
                DialogueManager.Instance.StartDialogue(quest.dialogue);
                QuestHolder.Instance.QuestAccept(quest, quest.Name);
                isHaveAvaliableQuest = true;
                break;
            }

            if(quest.PhaseCount > 0)
            {
                QuestPhaseScriptable questPhase = quest.QuestPhasesScriptable[quest.CurrentPhase - 1];

                if (questPhase.IsTalkQuest && PlayerStats.instance.CurrentLvl >= quest.LvlToStart)
                {
                    if (!questPhase.IsCompleted && questPhase.IsActive && questPhase.NPCToCommit == NPCScriptableObject)
                    {
                        ForStartingDialogue();
                        DialogueManager.Instance.StartDialogue(questPhase.dialogue);
                        QuestHolder.Instance.ReplaceQuest(quest);
                        QuestHolder.Instance.QuestProgress();
                    }
                    else if (!questPhase.IsCompleted && questPhase.IsActive && questPhase.NPCToCommit != NPCScriptableObject)
                    {
                        ForStartingDialogue();
                        DialogueManager.Instance.StartDialogue(questPhase.dialogue);
                    }
                    isHaveAvaliableQuest = true;
                    break;
                }
                else
                    continue;
            }          
        }
        if (!isHaveAvaliableQuest)
        {
            DialogueManager.Instance.StartDialogue(NPCScriptableObject.dialogues[0]);
        }
    }

    private void ForStartingDialogue()
    {
        if (PressButtonTip != null)
            PressButtonTip.SetActive(false);

        playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void InstantiateName()
    {
        _collider = GetComponent<Collider>();

        float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
        Vector3 healthBarPos = new Vector3(transform.position.x, colliderTop + 0.5f, transform.position.z);

        NameTextGO = Instantiate(NameTextGO, healthBarPos, Quaternion.identity, transform);
        _nameText = NameTextGO.GetComponentInChildren<TextMeshProUGUI>();
        _nameText.text = $"{NPCScriptableObject.Name}";
    }
}

