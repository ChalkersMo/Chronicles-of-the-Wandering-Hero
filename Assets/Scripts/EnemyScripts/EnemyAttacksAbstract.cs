using UnityEngine;

public abstract class EnemyAttacksAbstract : MonoBehaviour
{
    public int AttackCount;

    [SerializeField] protected Collider _defaultAttackCollider;
    [SerializeField] protected Collider _specialAttackCollider;

    public virtual void DeafaultAttack()
    {
        _defaultAttackCollider.enabled = true;
        AttackCount++;
    }
    public virtual void SpecialAttack()
    {
        _specialAttackCollider.enabled = true;
        AttackCount++;
    }
    public virtual void RenewAttack()
    {
        _defaultAttackCollider.enabled = false;
        _specialAttackCollider.enabled = false;
    }
}