using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAbstract : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [SerializeField] protected float sightRange, attackRange;
    [SerializeField] protected float timeBetweenAttacks;

    protected NavMeshAgent agent;
    protected Transform player;

    protected bool canAttack;
    protected bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Patroling()
    {

    }

    protected virtual void Chasing()
    {

    }

    protected virtual void Attacking()
    {

    }

    protected virtual void RenewAttack()
    {

    }

    public virtual void TakeDamage(float damage)
    {

    }
}
