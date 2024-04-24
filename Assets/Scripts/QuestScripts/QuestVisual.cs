using Cinemachine;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestVisual : MonoBehaviour
{

    [Space, Header("Quest info fields")]
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questPhaseName;
    [SerializeField] private TextMeshProUGUI questPhaseDescription;

    [SerializeField] private Image questImage;
    [SerializeField] private Image questProgressImage;

    [Space]
    [SerializeField] private ContentSizeFitter questParentContent;

    [Space]
    [SerializeField] private GameObject questTemplate;

    [Space, Header("Components to off with activating quests panel")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;

    [HideInInspector] public Transform panelQuest;

    [HideInInspector] public bool isActive;

    public List<QuestItem> QuestItems = new List<QuestItem>();
    
    private Canvas thisCanvas;

    private TextMeshProUGUI miniQuestPhaseDescription;

    private void Awake()
    {
        thisCanvas = GetComponent<Canvas>();
        miniQuestPhaseDescription = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        panelQuest = transform.GetChild(1);
        panelQuest.DOScale(0, 0);
        isActive = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!isActive)
                OnQuestsPanel();
            else
                OffQuestsPanel();
        }
    }
    public void OnQuestsPanel()
    {
        OffAllUI.Instance.OffUI();
        CanvasesSortingOrder.Instance.ShowOnFirst(thisCanvas);

        panelQuest.DOScale(1, 1);
        questName.transform.DOScale(0, 0);
        questDescription.transform.DOScale(0, 0);
        questImage.transform.DOScale(0, 0);
        questProgressImage.DOFillAmount(0, 0);
        questPhaseName.transform.DOScale(0, 0);
        questPhaseDescription.transform.DOScale(0, 0);
        playerController.enabled = false;
        thirdPersonCamera.enabled = false;
        CanvasesSortingOrder.Instance.ShowOnFirst(thisCanvas);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Invoke(nameof(ShowQuestsProgressInfo), 1);

        isActive = true;
    }
    public void OffQuestsPanel()
    {
        panelQuest.DOScale(0, 1);

        questName.transform.DOScale(0, 0);
        questDescription.transform.DOScale(0, 0);
        questImage.transform.DOScale(0, 0);
        questProgressImage.DOFillAmount(0, 0);
        questPhaseName.transform.DOScale(0, 0);
        questPhaseDescription.transform.DOScale(0, 0);
        questName.text = null;
        questDescription.text = null;
        questImage.sprite = null;
        questPhaseName.text = null;
        questPhaseDescription.text = null;

        Invoke(nameof(HideQuestsProgressInfo), 1);
        playerController.enabled = true;
        thirdPersonCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isActive = false;
    }
    public void QuestAccept(QuestScriptable questScriptable)
    {
        GameObject newQuest  = Instantiate(questTemplate, questParentContent.transform);
        QuestItem item = newQuest.GetComponent<QuestItem>();
        item.AssingFields(this, questScriptable);

        if (questScriptable.PhaseCount > 0)
        {
            QuestPhaseScriptable questPhase = questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1];
            miniQuestPhaseDescription.text = $"{questPhase.Description}: {questPhase.ProgressPoints}/{questPhase.PointsToComplete}";
        }
        else
            miniQuestPhaseDescription.text = $"{questScriptable.Description}: {questScriptable.ProgressPoints}/{questScriptable.PointsToComplete}";
    }
    public void QuestProgress(QuestScriptable questScriptable)
    {
        if (questScriptable.PhaseCount > 0)
        {
            QuestPhaseScriptable questPhase = questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1];
            miniQuestPhaseDescription.text = $"{questPhase.Description}: {questPhase.ProgressPoints}/{questPhase.PointsToComplete}";
        }
        else
            miniQuestPhaseDescription.text = $"{questScriptable.Description}: {questScriptable.ProgressPoints}/{questScriptable.PointsToComplete}";
    }
    public void QuestComplete(QuestScriptable questScriptable)
    {
        miniQuestPhaseDescription.text = null;
        ShowQuestinfo(questScriptable);
    }
    public void ShowQuestinfo(QuestScriptable questScriptable)
    {
        questName.transform.DOScale(0, 0);
        questDescription.transform.DOScale(0, 0);
        questImage.transform.DOScale(0, 0);

        questName.text = questScriptable.Name;
        questDescription.text = questScriptable.Description;
        questImage.sprite = questScriptable.QuestImage;

        questProgressImage.DOFillAmount(0, 0);
        float fillAmount = questScriptable.ProgressPoints / questScriptable.PointsToComplete;
        questProgressImage.DOFillAmount(fillAmount, 1);

        questName.transform.DOScale(1, 0.1f);
        questDescription.transform.DOScale(1, 0.1f);
        questImage.transform.DOScale(1, 0.1f);

        if (questScriptable.PhaseCount > 0)
        {
            questPhaseName.transform.DOScale(0, 0);
            questPhaseDescription.transform.DOScale(0, 0);
            QuestPhaseScriptable questPhase = questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1];
            questPhaseName.transform.DOScale(1, 0.1f);
            questPhaseDescription.transform.DOScale(1, 0.1f);
            questPhaseName.text = questPhase.Name;
            questPhaseDescription.text = $"{questPhase.Description}: {questPhase.ProgressPoints}/{questPhase.PointsToComplete}";
        }
        else
        {
            questPhaseName.text = null;
            questPhaseDescription.text = null;
        }
    }
    private void ShowQuestsProgressInfo()
    {
        foreach (QuestItem item in QuestItems)
        {
            item.ShowQuestProgress();
        }
    }
    private void HideQuestsProgressInfo()
    {
        foreach (QuestItem item in QuestItems)
        {
            item.HideQuestProgress();
        }
    }

}
