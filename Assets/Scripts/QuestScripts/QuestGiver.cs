using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestScriptable questScriptable;

    public void GiveQuest()
    {
        if(questScriptable.LvlToStart > 0)
            QuestHolder.Instance.QuestAccept(questScriptable, questScriptable.Name);          
    }
}
