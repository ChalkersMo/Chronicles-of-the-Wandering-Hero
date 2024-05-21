using UnityEngine;

public class QuestProgresser : MonoBehaviour
{
    [Header("If on enemy don't assing the variables, just set up an EnemyController")]
    [SerializeField] private QuestScriptable questScriptable;
    [SerializeField] private QuestPhaseScriptable questPhase;

    private QuestHolder questHolder;

    private void Start()
    {
        questHolder = FindObjectOfType<QuestHolder>();
    }

    public void ProgressQuest()
    {
        if (questScriptable.PhaseCount > 0)
        {
            if (TryGetComponent(out EnemyController enemy))
            {
                if (enemy.enemyScriptable == questHolder.tempQuestPhaseScriptable.EnemyToKill)
                    QuestHolder.Instance.QuestProgress();
            }
            else if (questScriptable.IsAccepted && questScriptable.IsActive)
            {
                try
                {
                    if (questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1] == questPhase)
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
        else
        {
            if (TryGetComponent(out EnemyController enemy))
            {
                if (enemy.enemyScriptable == questHolder.tempQuestScriptable.EnemyToKill)
                    QuestHolder.Instance.QuestProgress();
            }
            else if (questScriptable.IsAccepted && questScriptable.IsActive)
            {
                try
                {
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
}
