using UnityEngine;

public abstract class EnemyAbstractHit : MonoBehaviour
{
    private Collider thisCollider;
    protected PlayerDamageable playerDamageable;
    [SerializeField] protected Enemy enemyScriptable;
    [SerializeField] protected bool isDefaultAttack;

    private void Awake()
    {
        thisCollider = GetComponent<Collider>();
        playerDamageable = FindObjectOfType<PlayerDamageable>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thisCollider.enabled = false;

            if(isDefaultAttack)
                playerDamageable.TakeDamage(enemyScriptable.DefaultDamage);
            else
                playerDamageable.TakeDamage(enemyScriptable.SpecialDamage);
        }
    }
}
