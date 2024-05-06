using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearAttack : EnemyAttacksAbstract
{
    private NavMeshAgent m_Agent;
    private Transform playerTransform;
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void DeafaultAttack()
    {
        base.DeafaultAttack();
    }
    public override void SpecialAttack()
    {
        base.SpecialAttack();
        m_Agent.SetDestination(playerTransform.position);
        m_Agent.speed = 5;
    }
    public override void RenewAttack()
    {
        base.RenewAttack();
    }
}
