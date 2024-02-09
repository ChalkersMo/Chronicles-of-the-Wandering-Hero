using UnityEngine;

[CreateAssetMenu(fileName = "new Sword", menuName = "Sword")]
public class SwordItem : ScriptableObject
{
    public string Name;
    public string Description;
    public float Damage;

    public GameObject Prefab;
}
