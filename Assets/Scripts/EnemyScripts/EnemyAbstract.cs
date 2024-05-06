using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAbstract : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [SerializeField] protected float sightRange, attackRange;
    [SerializeField] protected float timeBetweenAttacks;

    protected EnemyDamageable enemyDamageable;
    protected NavMeshAgent agent;
    protected Transform player;

    protected bool canAttack = true;
    protected bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyDamageable = GetComponent<EnemyDamageable>();
    }

    protected virtual void Patroling()
    {
        
    }

    protected virtual void Chasing()
    {
        agent.SetDestination(player.position);
    }

    protected virtual void Attacking()
    {
        transform.LookAt(player);

        if (canAttack)
        {
            canAttack = false;
            Invoke(nameof(RenewAttack), timeBetweenAttacks);
        }
    }

    protected virtual void RenewAttack()
    {
        canAttack = true;
    }

    public virtual void TakeDamage(float damage)
    {
        enemyDamageable.TakeDamage(damage);
    }
}
