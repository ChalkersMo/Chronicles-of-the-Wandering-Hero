using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(Collider))]
public class NPCDialogueZone : MonoBehaviour
{
    [SerializeField] private GameObject questImage;   
    
    [SerializeField] private NPC NPCScriptableObject;
    [SerializeField] private GameObject PressButtonTip;
    [SerializeField] private GameObject NameTextGO;

    private Collider _collider;
    private TextMeshProUGUI _nameText;

    private DialogueManager dialogueManager;
    private QuestScriptable tempQuest;

    private bool isPlayerNear = false;

   private void Start()
    {
        _collider = GetComponent<Collider>();
        InstantiateName();
        InstantiateQuestImage();
        IsHaveQuest(PlayerStats.instance.CurrentLvl);
        dialogueManager = FindObjectOfType<DialogueManager>();
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
            isPlayerNear = false;
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
            dialogueManager.StartDialogue(NPCScriptableObject.dialogues[0]);
        }

        if (PressButtonTip != null)
            PressButtonTip.SetActive(false);       
    }
    private void OnQuest()
    {
        bool isHaveAvaliableQuest = false;
        foreach (QuestScriptable quest in NPCScriptableObject.Quests)
        {
            if (!quest.IsCompleted && !quest.IsAccepted && PlayerStats.instance.CurrentLvl >= quest.LvlToStart)
            {
                tempQuest = quest;
                DialogueManager.Instance.StartDialogue(quest.dialogue);
                DialogueManager.EndOfDialogue += AcceptQuestAfterDialogue;
                isHaveAvaliableQuest = true;
                break;
            }
            else if(quest.IsCompleted)
                isHaveAvaliableQuest = false;

            if (quest.PhaseCount > 0)
            {
                QuestPhaseScriptable questPhase = quest.QuestPhasesScriptable[quest.CurrentPhase - 1];

                if (questPhase.IsTalkQuest && PlayerStats.instance.CurrentLvl >= quest.LvlToStart)
                {
                    tempQuest = quest;
                    if (!questPhase.IsCompleted && questPhase.IsActive && questPhase.NPCToCommit == NPCScriptableObject)
                    {
                        DialogueManager.Instance.StartDialogue(questPhase.dialogue);
                        DialogueManager.EndOfDialogue += ProgressQuestAfterDialogue;
                        isHaveAvaliableQuest = true;
                    }
                    else if (!questPhase.IsCompleted && questPhase.IsActive && questPhase.NPCToCommit != NPCScriptableObject)
                    {
                        DialogueManager.Instance.StartDialogue(questPhase.dialogue);
                        isHaveAvaliableQuest = true;
                    }
                    break;
                }
                else
                {
                    isHaveAvaliableQuest = false;
                    continue;
                }                    
            }          
        }
        if (!isHaveAvaliableQuest)
        {
            DialogueManager.Instance.StartDialogue(NPCScriptableObject.dialogues[0]);
        }
    }
    private void AcceptQuestAfterDialogue()
    {
        QuestHolder.Instance.QuestAccept(tempQuest, tempQuest.Name);
    }
    private void ProgressQuestAfterDialogue()
    {
        QuestHolder.Instance.ReplaceQuest(tempQuest);
        QuestHolder.Instance.QuestProgress();
    }
  
    private void InstantiateName()
    {

        float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
        Vector3 instantiatePos = new Vector3(transform.position.x, colliderTop + 0.5f, transform.position.z);

        NameTextGO = Instantiate(NameTextGO, instantiatePos, Quaternion.identity, transform);
        _nameText = NameTextGO.GetComponentInChildren<TextMeshProUGUI>();
        _nameText.text = $"{NPCScriptableObject.Name}";
    }
    private void InstantiateQuestImage()
    {
        if (NPCScriptableObject.Quests.Count > 0)
        {
            float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
            Vector3 instantiatePos = new Vector3(transform.position.x, colliderTop + 1.5f, transform.position.z);
            questImage = Instantiate(questImage, instantiatePos, Quaternion.identity, transform);
            questImage.SetActive(false);
        }
    }

    public void IsHaveQuest(int Lvl)
    {
        if(NPCScriptableObject.Quests.Count > 0)
        {
            bool isHaveQuest = false;
            foreach(QuestScriptable quest in NPCScriptableObject.Quests)
            {
                if (quest.LvlToStart <= Lvl && !quest.IsCompleted)
                {
                    isHaveQuest = true;
                    break;
                }
                else
                    isHaveQuest = false;

            }
            if (isHaveQuest)
                questImage.SetActive(true);
            else
                questImage.SetActive(false);
        }
    }
}

