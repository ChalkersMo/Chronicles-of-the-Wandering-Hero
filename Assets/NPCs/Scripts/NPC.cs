using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public string Name;
    public string Description;
    public int Level;
    public bool isContactable;
    public bool isEnemy;
    public bool isFriend;
    [TextArea(3, 10)]
    public string[] sentences;
}
