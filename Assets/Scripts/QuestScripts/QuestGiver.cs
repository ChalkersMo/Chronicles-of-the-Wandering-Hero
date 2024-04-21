using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestScriptable questScriptable;

    public void GiveQuest()
    {
        if(questScriptable.LvlToStart <= PlayerStats.instance.CurrentLvl)
            QuestHolder.Instance.QuestAccept(questScriptable, questScriptable.Name);          
    }
}
