using UnityEngine;

[RequireComponent(typeof(EnemyAttacksAbstract), typeof(EnemyAnimation))]
public class EnemyController : EnemyAbstract
{
    [SerializeField] private Enemy enemyScriptable;
    private EnemyAttacksAbstract _enemyAttack;
    private EnemyAnimation _enemyAnim;

    private Transform target;

    private readonly float distance;

    private bool _walkPointSet = false;
    private bool _isSeekingPoint = false;

    [SerializeField] private Transform[] targets;

    private void Start()
    {
        _enemyAttack = GetComponent<EnemyAttacksAbstract>();
        _enemyAnim = GetComponent<EnemyAnimation>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSpecialAttackRange = Physics.CheckSphere(transform.position, specialAttackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !playerInSpecialAttackRange)
            Patroling();
        else if (playerInSightRange && playerInAttackRange)
            Attacking();
        else if (playerInSightRange && playerInSpecialAttackRange && _enemyAttack.AttackCount % 3 == 0)
            Attacking();
        else if (playerInSightRange && !playerInAttackRange && canAttack) 
            Chasing();
    }
    protected override void Patroling()
    {
        if (!_walkPointSet && !_isSeekingPoint)
        {
            agent.SetDestination(transform.position);
            _isSeekingPoint = true;
            _enemyAnim.StandAnim();
            Invoke(nameof(SeekPatrolPoint), enemyScriptable.TimeToStand);          
        }         

        if (_walkPointSet)
        {
            agent.SetDestination(target.position);
            agent.speed = enemyScriptable.PatrolingSpeed;
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
        agent.speed = enemyScriptable.ChasingSpeed;
        _enemyAnim.RunAnim();
    }

    protected override void Attacking()
    {
        transform.LookAt(player);

        if (canAttack)
        {
            agent.SetDestination(transform.position);
            if (_enemyAttack.AttackCount % 3 != 0)
            {
                _enemyAttack.DeafaultAttack();
                canAttack = false;
                _enemyAnim.DefaultAttackAnim();
                Invoke(nameof(RenewAttack), enemyScriptable.DefaultAttackDuration);
            }
            else
            {
                _enemyAttack.SpecialAttack();
                canAttack = false;
                _enemyAnim.SpecialAttackAnim();
                Invoke(nameof(RenewAttack), enemyScriptable.SpecialAttackDuration);
            }          
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
        Gizmos.DrawWireSphere(transform.position, specialAttackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
