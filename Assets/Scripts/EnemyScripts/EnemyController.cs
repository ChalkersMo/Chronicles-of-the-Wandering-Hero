using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : EnemyAbstract
{
    private EnemyDamageable enemyDamageable;

    private Transform target;

    private readonly float distance;

    private bool _walkPointSet = false;
    private bool _isSeekingPoint = false;
    [SerializeField] private Transform[] targets;

    [SerializeField] private float timeToStand;
    [SerializeField] private float patrolingSpeed;
    [SerializeField] private float chasingSpeed;

    private void Start()
    {
        enemyDamageable = GetComponent<EnemyDamageable>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        else if (playerInSightRange && !playerInAttackRange) 
            Chasing();
        else  if (playerInAttackRange && playerInSightRange) 
            Attacking();

    }
    protected override void Patroling()
    {
        if (!_walkPointSet && !_isSeekingPoint)
        {
            agent.SetDestination(transform.position);
            _isSeekingPoint = true;
            Debug.Log("seeking");
            Invoke(nameof(SeekPatrolPoint), timeToStand);          
        }         

        if (_walkPointSet)
        {
            agent.SetDestination(target.position);
            agent.speed = patrolingSpeed;
        }

        if(target != null)
        {
            float distanceToWalkPoint = Vector3.Distance(transform.position, target.position);
            if (distanceToWalkPoint <= 1f)
                _walkPointSet = false;             
        }
    }
    private void SeekPatrolPoint()
    {
        int rand = Random.Range(0, targets.Length);
        target = targets[rand];
        _walkPointSet = true;
        _isSeekingPoint = false;
        Debug.Log("Yes!");
    }
    protected override void Chasing()
    {
        agent.SetDestination(player.position);
        agent.speed = chasingSpeed;
    }

    protected override void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (canAttack)
        {
            canAttack = false;
            Invoke(nameof(RenewAttack), timeBetweenAttacks);
        }
    }   

    protected override void RenewAttack()
    {
        canAttack = true;
    }

    public override void TakeDamage(float damage)
    {
        enemyDamageable.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
