using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestVisual : MonoBehaviour
{
    private Dictionary<string, GameObject> AcceptedQuests = new Dictionary<string, GameObject>();

    private Canvas thisCanvas;


    [Space, Header("Quest info fields")]
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private Image questImage;
    [Space]
    [SerializeField] private ContentSizeFitter questParentContent;

    [Space]
    [SerializeField] private GameObject questTemplate;

    private Transform panelQuest;

    [Space, Header("Components to off with activating quests panel")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject thirdPersonCamera;

    private bool isActive;

    private void Awake()
    {
        thisCanvas = GetComponent<Canvas>();
        panelQuest = transform.GetChild(0);
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
        panelQuest.DOScale(1, 1);
        playerController.enabled = false;
        thirdPersonCamera.SetActive(false);
        CanvasesSortingOrder.Instance.ShowOnFirst(thisCanvas);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isActive = true;
    }
    public void OffQuestsPanel()
    {
        panelQuest.DOScale(0, 1);
        questName.text = null;
        questDescription.text = null;
        questImage.sprite = null;
        playerController.enabled = true;
        thirdPersonCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isActive = false;
    }
    public void QuestAccept(QuestScriptable questScriptable)
    {
        GameObject newQuest  = Instantiate(questTemplate, questParentContent.transform);
        QuestItem item = newQuest.GetComponent<QuestItem>();
        item.AssingFields(this, questScriptable);

        AcceptedQuests.Add(questScriptable.Name, newQuest);
    }
    public void QuestComplete(QuestScriptable questScriptable)
    {
        Destroy(AcceptedQuests[questScriptable.Name]);
    }
    public void ShowQuestinfo(QuestScriptable questScriptable)
    {
        questName.text = questScriptable.Name;
        questDescription.text = questScriptable.Description;
        questImage.sprite = questScriptable.QuestImage;
    }
}
