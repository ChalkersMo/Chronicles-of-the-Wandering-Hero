using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    private TextMeshProUGUI questName;
    private Image completedImage;
    private Image isActiveImage;
    private Button showInfoButton;
    private QuestScriptable questScriptable;
    private QuestVisual questVisual;

    private void Awake()
    {
        showInfoButton = GetComponent<Button>();
        completedImage = transform.GetChild(1).GetComponent<Image>();
        isActiveImage = transform.GetChild(2).GetComponent<Image>();
        questName = showInfoButton.GetComponentInChildren<TextMeshProUGUI>();
        showInfoButton.onClick.AddListener(ShowQuestinfo);
    }
    public void AssingFields(QuestVisual _questVisual, QuestScriptable _questScriptable)
    {
        questVisual = _questVisual;
        questScriptable = _questScriptable;
        questName.text = questScriptable.Name;
        questVisual.QuestItems.Add(this);
    }
    public void ShowQuestinfo()
    {
        questVisual.ShowQuestinfo(questScriptable);       
    }
    public void ShowQuestProgress()
    {
        if (questScriptable.IsCompleted)
            completedImage.DOFillAmount(1, 1);
        else
            completedImage.DOFillAmount(0, 0);

        if (questScriptable.IsActive)
            isActiveImage.DOFillAmount(1, 1);
        else
            isActiveImage.DOFillAmount(0, 0);
    }
    public void HideQuestProgress()
    {
        completedImage.DOFillAmount(0, 0);
        isActiveImage.DOFillAmount(0, 0);
    }
}
