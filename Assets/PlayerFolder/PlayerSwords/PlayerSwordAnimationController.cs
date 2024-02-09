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
        if(animatorController.GetInteger("State") == 1)
        {
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime &&
            animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                playerSwordController.IsAttacking = false;
                playerController.CanMove = true;
                animatorController.SetBool("hit1", false);
            }
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime &&
                animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                playerSwordController.IsAttacking = false;
                playerController.CanMove = true;
                animatorController.SetBool("hit2", false);
            }
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime &&
                animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            {
                playerController.CanMove = true;
                playerSwordController.IsAttacking = false;
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
    void OnClick()
    {
        lastClickedTime = Time.time;
        numberOfCklicks++;
        if (numberOfCklicks == 1)
        {
            animatorController.SetBool("hit1", true);
            playerController.CanMove = false;
            playerSwordController.IsAttacking = true;
        }
        numberOfCklicks = Mathf.Clamp(numberOfCklicks, 0, 3);

        if (numberOfCklicks >= 2 && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime && animatorController
            .GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", true);
            playerController.CanMove = false;
            playerSwordController.IsAttacking = true;
        }
        if (numberOfCklicks >= 3 && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > cooldownTime && animatorController
            .GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", true);
            playerController.CanMove = false;
            playerSwordController.IsAttacking = true;
        }
    }
}
