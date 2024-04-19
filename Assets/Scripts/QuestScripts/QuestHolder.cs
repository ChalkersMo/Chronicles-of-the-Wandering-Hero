using System.Collections.Generic;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
    public static QuestHolder Instance;

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
    }
    
    public bool QuestAccept(QuestScriptable questScriptable, string questName)
    {
        if (AcceptedQuests.ContainsKey(questName))
            return false;
        else
        {
            AcceptedQuests.Add(questName, questScriptable);
            questScriptable.IsAccepted = true;
            if (questScriptable.PhaseCount > 0)
                questScriptable.CurrentPhase++;
            Debug.Log("Accepted");
            return true;
        }
    }

    public void QuestProgress(QuestScriptable questScriptable)
    {
        if(questScriptable.PhaseCount > 0)
        {
            QuestPhaseProgress(questScriptable);
            return;
        }

        questScriptable.ProgressPoints++;
        if (questScriptable.ProgressPoints >= questScriptable.PointsToComplete)
            QuestCompleted(questScriptable);
    }

    private void QuestPhaseProgress(QuestScriptable questScriptable)
    {
        for (int i = 0; i < questScriptable.PhaseCount; i++)
        {
            QuestPhaseScriptable questPhase = questScriptable.QuestPhasesScriptable[i];

            if (!questPhase.IsCompleted)
            {
                questPhase.ProgressPoints++;

                if (questPhase.ProgressPoints >= questPhase.PointsToComplete)
                    QuestPhaseComplete(questScriptable, questPhase);

                return;
            }               
        }
    }

    private void QuestPhaseComplete(QuestScriptable questScriptable, QuestPhaseScriptable questPhase)
    {
        questPhase.IsCompleted = true;
        questPhase.IsActive = false;
        if (questPhase.IsLastTaskToDo)
            questPhase.NPCToTalk.isTaskCompleted = true;
        Debug.Log("FaseCompleted");
        if (questScriptable.QuestPhasesScriptable[questScriptable.PhaseCount - 1].IsCompleted)
        {
            QuestCompleted(questScriptable);
            return;
        }
        
        questScriptable.CurrentPhase++;
        questScriptable.QuestPhasesScriptable[questScriptable.CurrentPhase - 1].IsActive = true;
    }

    private void QuestCompleted(QuestScriptable questScriptable)
    {
        questScriptable.IsCompleted = true;
        questScriptable.IsAccepted = false;
        AcceptedQuests.Remove(questScriptable.Name);
        CompletedQuests.Add(questScriptable.Name, questScriptable);
        Debug.Log("QuestCompleted");
    }
}
