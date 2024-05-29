using UnityEngine;

[RequireComponent(typeof(EnemyAttacksAbstract), typeof(EnemyAnimation), typeof(EnemyAudio))]
public class EnemyController : EnemyAbstract
{
    public Enemy enemyScriptable;

    private EnemyAttacksAbstract _enemyAttack;
    private EnemyAnimation _enemyAnim;
    private AudioController _audioController;
    private EnemyAudio _enemyAudio;

    private Transform target;

    private readonly float distance;

    private bool _walkPointSet = false;
    private bool _isSeekingPoint = false;

    [SerializeField] private Transform[] targets;

    private void Start()
    {
        _enemyAttack = GetComponent<EnemyAttacksAbstract>();
        _enemyAnim = GetComponent<EnemyAnimation>();
        _enemyAudio = GetComponent<EnemyAudio>();
        _audioController = FindObjectOfType<AudioController>();
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
            _enemyAudio.RenewSource();
            Invoke(nameof(SeekPatrolPoint), enemyScriptable.TimeToStand);          
        }         

        if (_walkPointSet)
        {
            agent.SetDestination(target.position);
            agent.speed = enemyScriptable.PatrolingSpeed;
            _enemyAnim.WalkAnim();
            _enemyAudio.RenewSource();
            _enemyAudio.PlayWalkingSound();
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
        if(targets.Length != 0)
        {
            int rand = Random.Range(0, targets.Length);
            target = targets[rand];
            _walkPointSet = true;
            _isSeekingPoint = false;
        }
        else
        {
            _walkPointSet = false;
            _isSeekingPoint = false;
        }
    }
    protected override void Chasing()
    {
        agent.SetDestination(player.position);
        agent.speed = enemyScriptable.ChasingSpeed;
        _enemyAnim.RunAnim();
        _enemyAudio.RenewSource();
        _enemyAudio.PlayRunningSound();
        if (!_audioController.IsFightingTheme)
        {
            _audioController.ChangeTheme(null, 2, true, 0.8f);
            _audioController.IsFightingTheme = true;
        }
    }

    protected override void Attacking()
    {
        transform.LookAt(player);

        if (canAttack)
        {
            agent.SetDestination(transform.position);
            if (_enemyAttack.AttackCount % 3 != 0)
            {
                _enemyAudio.RenewSource();
                _enemyAudio.PlayAttackSound();
                _enemyAttack.DeafaultAttack();
                canAttack = false;
                _enemyAnim.DefaultAttackAnim();
                Invoke(nameof(RenewAttack), enemyScriptable.DefaultAttackDuration);
            }
            else
            {
                _enemyAudio.RenewSource();
                _enemyAudio.PlayUniqueAttackSound();
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
        _enemyAudio.RenewSource();
        _enemyAudio.PlayTakingdamageSound();
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
