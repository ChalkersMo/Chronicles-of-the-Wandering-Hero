using UnityEngine;

public class PlayerSwordAnimationController : MonoBehaviour
{
    EventBus eventBus;
    Animator animatorController;
    PlayerController playerController;
    PlayerSwordController playerSwordController;

    public float cooldownTime = 0.7f;
    float nextFireTime = 0f;
    public static int numberOfCklicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

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
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfCklicks = 0;
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", false);
        }
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
            if (playerController.SwordEquiped && !playerSwordController.IsAttacking)
            {               
                lastClickedTime = Time.time;
                numberOfCklicks++;
                if (numberOfCklicks == 1)
                {
                    animatorController.SetBool("hit1", true);
                }
                numberOfCklicks = Mathf.Clamp(numberOfCklicks, 0, 3);
                AfterAttack();
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

        AfterAttack();
    }

    void AfterAttack()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfCklicks = 0;
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", false);           
        }

        if (numberOfCklicks >= 2
            && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime
            && animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", true);
        }
        if (numberOfCklicks >= 3
            && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime
            && animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", true);
        }
        if(animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime
            && animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            animatorController.SetBool("hit3", false);
    }
}
