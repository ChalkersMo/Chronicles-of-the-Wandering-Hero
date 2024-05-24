using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool _isStanding;
    private bool _isWalking;
    private bool _isRunning;
    private bool _isAttacking;
    private bool _isSpecialAttacking;

    public void StandAnim()
    {
        if(animator != null && !_isStanding)
        {
            RenewBools();
            animator.SetTrigger("Stand");
            _isStanding = true;
        }
    }
    public void WalkAnim()
    {
        if (animator != null && !_isWalking)
        {
            RenewBools();
            animator.SetTrigger("Walk");
            _isWalking = true;
        }            
    }
    public void RunAnim()
    {
        if (animator != null && !_isRunning)
        {
            RenewBools();
            animator.SetTrigger("Run");
            _isRunning = true;
        }
    }
    public void DefaultAttackAnim()
    {
        if (animator != null && !_isAttacking)
        {
            RenewBools();
            animator.SetTrigger("AttackDefault");   
            _isAttacking = true;
        }
    }
    public void SpecialAttackAnim()
    {
        if (animator != null && !_isSpecialAttacking)
        {
            RenewBools();
            animator.SetTrigger("AttackSpecial");
            _isSpecialAttacking = true;
        }           
    }

    public void DieAnim()
    {
        RenewBools();
        animator.SetTrigger("Die");
    }
    public void RenewBools()
    {
        _isStanding = false;
        _isWalking = false;
        _isRunning = false;
        _isAttacking = false;
        _isSpecialAttacking = false;
    }
}
