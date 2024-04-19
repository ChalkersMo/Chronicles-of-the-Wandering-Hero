using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    private TextMeshProUGUI questName;
    private Button showInfoButton;
    private QuestScriptable questScriptable;
    private QuestVisual questVisual;

    private void Awake()
    {
        showInfoButton = GetComponent<Button>();
        questName = showInfoButton.GetComponentInChildren<TextMeshProUGUI>();
        showInfoButton.onClick.AddListener(ShowQuestinfo);
    }
    public void AssingFields(QuestVisual _questVisual, QuestScriptable _questScriptable)
    {
        questVisual = _questVisual;
        questScriptable = _questScriptable;
        questName.text = questScriptable.Name;
    }
    private void ShowQuestinfo()
    {
        questVisual.ShowQuestinfo(questScriptable);
    }
}
