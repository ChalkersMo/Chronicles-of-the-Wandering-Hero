using UnityEngine;

public class PlayerSwordController : MonoBehaviour
{
    Animator animatorController;
    PlayerController playerController;

    public float cooldownTime = 1.5f;
    float nextFireTime = 0f;
    public static int numberOfCklicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 2;

    private void Start()
    {
        animatorController = GetComponentInChildren<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(animatorController.GetInteger("State") == 1)
        {
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
            animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                animatorController.SetBool("hit1", false);
            }
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
                animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
            {
                animatorController.SetBool("hit2", false);
            }
            if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f &&
                animatorController.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
            {
                animatorController.SetBool("hit3", false);
                numberOfCklicks = 0;
            }

            if (Time.time - lastClickedTime > maxComboDelay && playerController.SwordEquiped != false)
            {
                numberOfCklicks = 0;
                playerController.CanMove = true;
            }
            if (Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnClick();
                    playerController.CanMove = false;
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
        }
        numberOfCklicks = Mathf.Clamp(numberOfCklicks, 0, 3);

        if (numberOfCklicks >= 2 && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animatorController
            .GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animatorController.SetBool("hit1", false);
            animatorController.SetBool("hit2", true);
        }
        if (numberOfCklicks >= 3 && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animatorController
            .GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animatorController.SetBool("hit2", false);
            animatorController.SetBool("hit3", true);
        }
    }
}
