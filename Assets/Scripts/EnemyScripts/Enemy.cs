using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
 
public class Enemy : ScriptableObject
{
    public string Name;
    public string Description;

    [Space]
    public float MaxHp;

    [Space, Header("Attack stats")]
    public float DefaultDamage;
    public float SpecialDamage;

    [Space]
    public float DefaultAttackDuration;
    public float SpecialAttackDuration;

    [Space, Header("Speed stats")]
    public float TimeToStand;
    public float PatrolingSpeed;
    public float ChasingSpeed;

    [Space, Header("Enemy prefab")]
    public GameObject Prefab;

    [Space, Header("Health bar settings")]
    public GameObject HealthBar;
    public Gradient HealthBarColorGradient;
}
