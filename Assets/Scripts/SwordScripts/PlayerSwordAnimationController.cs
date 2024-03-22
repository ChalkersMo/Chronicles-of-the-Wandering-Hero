using UnityEngine;

public class PlayerSwordAnimationController : MonoBehaviour
{
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
        animatorController = GetComponentInChildren<Animator>();
        playerController = FindObjectOfType<PlayerController>();
        playerSwordController = FindObjectOfType<PlayerSwordController>();
    }

    private void Update()
    {
        if(animatorController.GetInteger("State") == 1 && playerSwordController != null)
        {
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime &&
            animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                animatorController.SetBool("hit1", false);
            }
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime &&
                animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                animatorController.SetBool("hit2", false);
            }
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime &&
                animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            {
                animatorController.SetBool("hit3", false);
                numberOfCklicks = 0;
            }

            if (Time.time - lastClickedTime > maxComboDelay)
            {
                numberOfCklicks = 0;
            }
            if (Time.time > nextFireTime && playerController.SwordEquiped != false && playerSwordController.IsAttacking == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnClick();
                }
            }
        }       
    }
    public void AssignSwordController(PlayerSwordController controller)
    {
        playerSwordController = controller;
    }
    void OnClick()
    {
        lastClickedTime = Time.time;
        numberOfCklicks++;
        if (numberOfCklicks == 1)
        {
            animatorController.SetBool("hit1", true);
        }
        numberOfCklicks = Mathf.Clamp(numberOfCklicks, 0, 3);

        if (numberOfCklicks >= 2 && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime && animatorController
            .GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", true);
        }
        if (numberOfCklicks >= 3 && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime && animatorController
            .GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", true);
            
        }
    }

    public void Attacking()
    {
        playerSwordController.IsAttacking = true;
    }
    public void DisableMoving()
    {
        playerController.CanMove = false;
    }
    public void EnableMoving()
    {
        playerController.CanMove = true;
    }
    public void DisAttacking()
    {
        playerSwordController.IsAttacking = false;
    }
}
