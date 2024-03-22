using UnityEngine;

[CreateAssetMenu(fileName = "new Sword", menuName = "Sword")]
public class SwordItem : ScriptableObject
{
    public string Name;
    public string Description;
    public float Damage;
    public bool IsMain;

    public Sprite Sprite;
    public GameObject Prefab;
    public Color DamagePopUpFaceColor;
    public Color DamagePopUpoutlineColor;
}
