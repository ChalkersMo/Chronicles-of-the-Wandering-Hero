using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimController : MonoBehaviour
{
    Animator animatorController;
    Vector2 animMove;

    bool ReadyToHit;
    bool ReadyToHit2;
    bool ReadyToHit3;

    private void Start()
    {
        animatorController = GetComponentInChildren<Animator>();
    }
    public void WalkAnim()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        animMove = new Vector2(horizontal, vertical);
        animatorController.SetFloat("X", animMove.x, 0.1f, Time.deltaTime);
        animatorController.SetFloat("Y", animMove.y, 0.1f, Time.deltaTime);
        animatorController.SetFloat("SpeedMultiplier", 1);
    }
    public void RunAnim()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        animMove = new Vector2(horizontal, vertical);
        animatorController.SetFloat("X", animMove.x, 0.1f, Time.deltaTime);
        animatorController.SetFloat("Y", animMove.y, 0.1f, Time.deltaTime);
        animatorController.SetFloat("SpeedMultiplier", 1.3f);
    }
    public void StandAnim()
    {
        float vertical = 0;
        float horizontal = 0;
        animMove = new Vector2(horizontal, vertical);
        animatorController.SetFloat("X", animMove.x, 0, Time.deltaTime);
        animatorController.SetFloat("Y", animMove.y, 0, Time.deltaTime);
    }
    public void JumpAnim()
    {
        animatorController.SetTrigger("Jump");
    }
    public void AttackAnim(int numberOfCklicks)
    {
        if (numberOfCklicks >= 0 && ReadyToHit)
        {
            animatorController.SetBool("hit1", true);
            ReadyToHit = false;
            return;
        }
        if (numberOfCklicks >= 2
          && !animatorController.GetBool("hit1")
          && ReadyToHit2)
        {
            animatorController.SetBool("hit2", true);
            ReadyToHit2 = false;
            return;
        }
        if (numberOfCklicks >= 3
            && !animatorController.GetBool("hit2")
            && ReadyToHit3)
        {
            animatorController.SetBool("hit3", true);
            ReadyToHit3 = false;
            return;
        }
    }
    public void AfterAttackAnim()
    {
        if (animatorController.GetBool("hit1"))
        {
            animatorController.SetBool("hit1", false);
            ReadyToHit2 = true;
            return;
        }
        if (animatorController.GetBool("hit2"))
        {
            animatorController.SetBool("hit2", false);
            ReadyToHit3 = true;
            return;
        }
        if (animatorController.GetBool("hit3"))
        {
            animatorController.SetBool("hit3", false);
            ReadyToHit2 = false;
            ReadyToHit3 = false;
            ReadyToHit = true;
            return;
        }

    }

    public void ComboRenewAnim()
    {
        animatorController.SetBool("hit1", false);
        animatorController.SetBool("hit2", false);
        animatorController.SetBool("hit3", false);
        ReadyToHit2 = false;
        ReadyToHit3 = false;
        ReadyToHit = true;
    }
    public void SetBoolsAnim(bool groundedPlayer, bool IsRunning)
    {
        animatorController.SetBool("IsGrounded", groundedPlayer);
        animatorController.SetBool("IsRunning", IsRunning);
    }
    public void EquipSwordAnim()
    {
        animatorController.SetInteger("State", 1);
    }
    public void SwordDisEquipingAnim()
    {
        animatorController.SetInteger("State", 0);
        animatorController.SetBool("hit1", false);
        animatorController.SetBool("hit2", false);
        animatorController.SetBool("hit3", false);
    }
}
