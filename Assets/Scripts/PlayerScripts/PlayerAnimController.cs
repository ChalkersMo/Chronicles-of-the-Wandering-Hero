using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimController : MonoBehaviour
{
    Animator animatorController;
    Vector2 animMove;

    private void Start()
    {
        animatorController = GetComponentInChildren<Animator>();
    }

    public void Walk()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        animMove = new Vector2(horizontal, vertical);
        animatorController.SetFloat("X", animMove.x, 0.1f, Time.deltaTime);
        animatorController.SetFloat("Y", animMove.y, 0.1f, Time.deltaTime);
        animatorController.SetFloat("SpeedMultiplier", 1);
    }
    public void Run()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        animMove = new Vector2(horizontal, vertical);
        animatorController.SetFloat("X", animMove.x, 0.1f, Time.deltaTime);
        animatorController.SetFloat("Y", animMove.y, 0.1f, Time.deltaTime);
        animatorController.SetFloat("SpeedMultiplier", 1.3f);
    }
    public void Jump()
    {
        animatorController.SetTrigger("Jump");
    }
    public void Attack()
    {

    }
    public void SetBools(bool groundedPlayer, bool IsRunning)
    {
        animatorController.SetBool("IsGrounded", groundedPlayer);
        animatorController.SetBool("IsRunning", IsRunning);
    }
    public void EquipSword()
    {
        animatorController.SetInteger("State", 1);
    }
    public void SwordDisEquiping()
    {
        animatorController.SetInteger("State", 0);
    }
}
