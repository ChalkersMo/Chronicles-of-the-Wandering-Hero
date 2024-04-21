using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public string Name;
    [TextArea(3, 10)]
    public string Description;

    public int Level;

    public bool isEnemy;
    public bool isFriend;

    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<QuestScriptable> Quests = new List<QuestScriptable>();
}
