using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
 
public class Enemy : ScriptableObject
{
    public string Name;
    public string Description;
    public float MaxHp;
    public GameObject Prefab;
    public GameObject HealthBar;
    public Gradient HealthBarColorGradient;
}
