using UnityEngine;
using DG.Tweening;
using TMPro;
[RequireComponent(typeof(Collider))]
public class NPCDialogueZone : MonoBehaviour
{
    Collider _collider;

    [SerializeField] NPC NPCScriptableObject;

    [SerializeField] GameObject NPCGameObj;
    [SerializeField] GameObject PressButtonTip;
    [SerializeField] GameObject NameTextGO;

    TextMeshProUGUI _nameText;

    DialogueManager dialogueManager;
    PlayerController playerController;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
        Vector3 healthBarPos = new Vector3(transform.position.x, colliderTop + 0.5f, transform.position.z);
        NameTextGO = Instantiate(NameTextGO, healthBarPos, Quaternion.identity, transform);
        _nameText = NameTextGO.GetComponentInChildren<TextMeshProUGUI>();
        _nameText.text = $"{NPCScriptableObject.Name}";
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PressButtonTip != null)
            {
                PressButtonTip.SetActive(true);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NPCGameObj.transform.DORotate(other.transform.position, 1);
            if (Input.GetKey(KeyCode.F))
            {
                StartingDialogue();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PressButtonTip != null)
            {
                PressButtonTip.SetActive(false);
            }
        }
    }
    private void StartingDialogue()
    {
        OnQuest();
        if (PressButtonTip != null)
            PressButtonTip.SetActive(false);

        playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dialogueManager.StartDialogue(NPCScriptableObject);
    }
    private void OnQuest()
    {
        if (TryGetComponent(out QuestGiver questGiver))
        {
            if (!questGiver.questScriptable.IsCompleted && !questGiver.questScriptable.IsAccepted)
                DialogueManager.EndOfDialogue += questGiver.GiveQuest;
        }
        if (TryGetComponent(out QuestProgresser questProgresser))
        {
            QuestScriptable _questScriptable = questProgresser.questScriptable;
            if (_questScriptable.IsAccepted)
            {
                QuestPhaseScriptable _currentQuestPhase = _questScriptable.QuestPhasesScriptable[_questScriptable.CurrentPhase - 1];

                if (_currentQuestPhase.IsTalkQuest
                    && _currentQuestPhase.NPCToTalk == NPCScriptableObject)
                {
                    DialogueManager.EndOfDialogue += questProgresser.ProgressQuest;
                }
            }
        }
    }
}

