using UnityEngine;

public class QuestProgresser : MonoBehaviour
{
    [Header("If on enemy don't assing the variables, just set up an EnemyController")]
    [SerializeField] private QuestScriptable questScriptable;
    [SerializeField] private QuestPhaseScriptable questPhase;

    public void ProgressQuest()
    {
        if (TryGetComponent(out EnemyController enemy))
        {
            if (enemy.enemyScriptable == QuestHolder.Instance.tempQuestPhaseScriptable.EnemyToKill)
                QuestHolder.Instance.QuestProgress();
        }
        else
        {
            try
            {
                if(questScriptable.IsAccepted && questScriptable.IsActive && questScriptable.PhaseCount > 0)
                {
                    if (questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1] == questPhase)
                        QuestHolder.Instance.QuestProgress();
                }
                else if(questScriptable.IsAccepted && questScriptable.IsActive)
                    QuestHolder.Instance.QuestProgress();
            }
            catch
            {
                Debug.LogError("questScriptable or questPhase not set " +
                   "or there is no EnemyController script on the object");
                return;
            }
        }
    }
}
