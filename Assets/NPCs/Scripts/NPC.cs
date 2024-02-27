using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public string Name;
    [TextArea(3, 10)]
    public string Description;
    public int Level;
    public bool isContactable;
    public bool isEnemy;
    public bool isFriend;
    public bool isTaskCompleted;
    [TextArea(3, 10)]
    public string[] sentences1;
    [TextArea(3, 10)]
    public string[] sentences2;
}
