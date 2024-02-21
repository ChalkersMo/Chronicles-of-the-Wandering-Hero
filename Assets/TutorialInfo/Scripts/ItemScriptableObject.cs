using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default}

public class ItemScriptableObject : ScriptableObject
{

    public string itemName;
    public int maximumAmount;
    public string itemDescription;

}
