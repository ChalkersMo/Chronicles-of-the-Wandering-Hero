using UnityEngine;

public class PlayerSwordAnimationController : MonoBehaviour
{
    EventBus eventBus;
    Animator animatorController;
    PlayerController playerController;
    PlayerSwordController playerSwordController;

    public float cooldownTime = 1.2f;
    public static int numberOfCklicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 2;

    private void Start()
    {
        eventBus = EventBus.Instance;
        animatorController = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();

        eventBus.OnAttack += StartAttacking;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
        if (animatorController.GetInteger("State") == 1 && playerSwordController != null)
            ComboRenewCheck();
    }

    public void AssignSwordController(PlayerSwordController controller)
    {
        playerSwordController = controller;

        if(playerSwordController != null)
            animatorController.SetInteger("State", 1);
        else
        {
            animatorController.SetInteger("State", 0);
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", false);
        }
    }
    void OnClick()
    {
        eventBus.OnAttack?.Invoke();       
    }

    void StartAttacking()
    {
        if (animatorController.GetInteger("State") == 1 && playerSwordController != null)
        {
            if (playerController.SwordEquiped && !playerSwordController.IsAttacking && Time.time - lastClickedTime > cooldownTime)
            {               
                lastClickedTime = Time.time;
                numberOfCklicks++;               
                numberOfCklicks = Mathf.Clamp(numberOfCklicks, 0, 3);
                AttackAnim();
                playerController.CanMove = false;
            }
        }
    } 
 
    public void EnableAttacking()
    {
        if (playerSwordController != null)
            playerSwordController.IsAttacking = true;
    }
    public void DisableAttacking()
    {
        if (playerSwordController != null)
            playerSwordController.IsAttacking = false;
    }
    public void EndAttacking()
    {
        if (playerSwordController != null)
            playerController.CanMove = true;
    }

    void AttackAnim()
    {
        if (numberOfCklicks == 1)
        {
            animatorController.SetBool("hit1", true);
        }
        if (numberOfCklicks >= 2
          && animatorController.GetBool("hit1"))
        {
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", true);
        }
        if (numberOfCklicks >= 3
            && animatorController.GetBool("hit2"))
        {
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", true);
        }
    }

    void ComboRenewCheck()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfCklicks = 0;
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", false);
            EndAttacking();
            playerSwordController.IsAttacking = false;
        }
    }
}
