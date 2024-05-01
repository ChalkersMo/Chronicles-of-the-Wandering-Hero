using UnityEngine;

public class QuestProgresser : MonoBehaviour
{
    [SerializeField] private QuestScriptable questScriptable;
    [SerializeField] private QuestPhaseScriptable questPhase;

    public void ProgressQuest()
    {
        if(questScriptable.IsAccepted && questScriptable.IsActive)
        {
            if(questScriptable.PhaseCount > 0)
            {
                if (questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1] == questPhase)
                    QuestHolder.Instance.QuestProgress();
            }
            else
                QuestHolder.Instance.QuestProgress();
        }
            
    }
}
