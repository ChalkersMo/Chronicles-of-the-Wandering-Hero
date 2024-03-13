using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Item")]
public class ItemScriptable : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public bool Stackable;
    public int Quantity;
    public GameObject TipToPickUp;
}
