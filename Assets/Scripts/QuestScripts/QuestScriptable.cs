using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestScriptable : ScriptableObject
{
    public QuestPhaseScriptable[] QuestPhasesScriptable;

    [Space]
    public string Name;
    public string Description;

    [Space, Header("Must be equal to QuestPhasesScriptable count")]
    public int PhaseCount;

    [Space]
    public int CurrentPhase;
    public int LvlToStart;
    public int PointsToComplete;
    public int ProgressPoints;

    [Space]
    public bool IsAccepted;
    public bool IsCompleted;
}
