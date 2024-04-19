using UnityEngine;

public class QuestProgresser : MonoBehaviour
{
    public QuestScriptable questScriptable;

    public void ProgressQuest()
    {
        if(questScriptable.IsAccepted && questScriptable.IsActive)
            QuestHolder.Instance.QuestProgress();
    }
}
