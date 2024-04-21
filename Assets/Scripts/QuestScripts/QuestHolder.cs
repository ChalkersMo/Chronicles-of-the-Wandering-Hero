using System.Collections.Generic;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
    public static QuestHolder Instance;
    private QuestVisual questVisual;
    private QuestScriptable tempQuestScriptable;

    private Dictionary<string, QuestScriptable> AcceptedQuests = new Dictionary<string, QuestScriptable>();
    private Dictionary<string, QuestScriptable> CompletedQuests = new Dictionary<string, QuestScriptable>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        questVisual = GetComponent<QuestVisual>();
    }
    
    public void QuestAccept(QuestScriptable questScriptable, string questName)
    {
        if (AcceptedQuests.ContainsKey(questName))
            return;
        else
        {
            AcceptedQuests.Add(questName, questScriptable);
            questScriptable.IsAccepted = true;
            ReplaceQuest(questScriptable);

           if (questScriptable.PhaseCount > 0)
                questScriptable.CurrentPhase++;

            questVisual.QuestAccept(questScriptable);
            return;
        }
    }

    public void QuestProgress()
    {
        if (tempQuestScriptable.PhaseCount > 0)
        {
            QuestPhaseProgress();
            return;
        }

        tempQuestScriptable.ProgressPoints++;
        questVisual.QuestProgress(tempQuestScriptable);
        if (tempQuestScriptable.ProgressPoints >= tempQuestScriptable.PointsToComplete)
            QuestCompleted(tempQuestScriptable);
    }

    private void QuestPhaseProgress()
    {
        for (int i = 0; i < tempQuestScriptable.PhaseCount; i++)
        {
            QuestPhaseScriptable questPhase = tempQuestScriptable.QuestPhasesScriptable[i];

            if (!questPhase.IsCompleted)
            {
                questPhase.ProgressPoints++;
                questVisual.QuestProgress(tempQuestScriptable);
                if (questPhase.ProgressPoints >= questPhase.PointsToComplete)
                    QuestPhaseComplete(questPhase);

                return;
            }               
        }
    }

    private void QuestPhaseComplete(QuestPhaseScriptable questPhase)
    {
        questPhase.IsCompleted = true;
        questPhase.IsActive = false;
        tempQuestScriptable.ProgressPoints++;
        questVisual.QuestProgress(tempQuestScriptable);

        if (tempQuestScriptable.QuestPhasesScriptable[tempQuestScriptable.PhaseCount - 1].IsCompleted)
        {
            QuestCompleted(tempQuestScriptable);
            return;
        }

        tempQuestScriptable.CurrentPhase++;
        tempQuestScriptable.QuestPhasesScriptable[tempQuestScriptable.CurrentPhase - 1].IsActive = true;
    }

    private void QuestCompleted(QuestScriptable questScriptable)
    {
        questScriptable.IsCompleted = true;
        questScriptable.IsAccepted = false;
        AcceptedQuests.Remove(questScriptable.Name);
        CompletedQuests.Add(questScriptable.Name, questScriptable);
        questVisual.QuestComplete(questScriptable);
    }

    public void ReplaceQuest(QuestScriptable questScriptable)
    {
        if (tempQuestScriptable == null)
        {
            tempQuestScriptable = questScriptable;
            questScriptable.IsActive = true;
        }           
        else
        {
            tempQuestScriptable.IsActive = false;            
            questScriptable.IsActive = true;
            tempQuestScriptable = questScriptable;
        }
    }
}
