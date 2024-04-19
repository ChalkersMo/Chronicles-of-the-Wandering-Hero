using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestPhase", menuName = "Quests/QuestPhase")]
public class QuestPhaseScriptable : ScriptableObject
{
    public string Description;
    [Space]
    public int ProgressPoints;
    public int PointsToComplete;
    [Space]
    public bool IsCompleted;
    public bool IsActive;
    public bool IsTalkQuest;
    public bool IsLastTaskToDo;
    [Space, Header("Put here an item of NPC which must give reward talking with him")]
    public NPC NPCToTalk;
}
