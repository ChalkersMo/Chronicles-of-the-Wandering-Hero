using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestPhase", menuName = "Quests/QuestPhase")]
public class QuestPhaseScriptable : ScriptableObject
{
    public string Name;
    public string Description;
    [Space]
    public int ProgressPoints = 0;
    public int PointsToComplete;
    [Space]
    public bool IsCompleted = false;
    public bool IsActive = false;
    public bool IsTalkQuest;
    [Space]
    public RewardScriptable[] Rewards;
    [Space, Header("If quest is talk quest create the dialogue")]
    public Dialogue dialogue;
    [Space, Header("If quest is talk quest with NPC for commiting this quest place here this NPC")]
    public NPC NPCToCommit;
}
