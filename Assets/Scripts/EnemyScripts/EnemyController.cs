using UnityEngine;

[RequireComponent(typeof(EnemyAttacksAbstract), typeof(EnemyAnimation))]
public class EnemyController : EnemyAbstract
{
    private EnemyAttacksAbstract _enemyAttack;
    private EnemyAnimation _enemyAnim;

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
        _enemyAttack = GetComponent<EnemyAttacksAbstract>();
        _enemyAnim = GetComponent<EnemyAnimation>();
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
            _enemyAnim.StandAnim();
            Invoke(nameof(SeekPatrolPoint), timeToStand);          
        }         

        if (_walkPointSet)
        {
            agent.SetDestination(target.position);
            agent.speed = patrolingSpeed;
            _enemyAnim.WalkAnim();
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
    }
    protected override void Chasing()
    {
        agent.SetDestination(player.position);
        agent.speed = chasingSpeed;
        _enemyAnim.RunAnim();
    }

    protected override void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (canAttack)
        {
            _enemyAttack.DeafaultAttack();
            canAttack = false;
            _enemyAnim.DefaultAttackAnim();
            Invoke(nameof(RenewAttack), timeBetweenAttacks);
        }
    }   

    protected override void RenewAttack()
    {
        base.RenewAttack();
        _enemyAnim.RenewBools();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
