using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestScriptable : ScriptableObject
{
    public QuestPhaseScriptable[] QuestPhasesScriptable;

    [Space]
    public Sprite QuestImage;

    [Space, Header("If there is no phases and need mark to show quest position place here")]
    public GameObject QuestMark;

    [Space]
    public string Name;
    public string Description;

    [Space, Header("Must be equal to QuestPhasesScriptable count")]
    public int PhaseCount;

    [Space]
    public int CurrentPhase = 0;
    public int LvlToStart;
    public float PointsToComplete;
    public float ProgressPoints = 0;

    [Space]
    public bool IsActive = false;
    public bool IsAccepted = false;
    public bool IsCompleted = false;
    [Space]
    public RewardScriptable[] Rewards;
    [Space, Header("Entering quest dialogue if need")]
    public Dialogue dialogue;
}
