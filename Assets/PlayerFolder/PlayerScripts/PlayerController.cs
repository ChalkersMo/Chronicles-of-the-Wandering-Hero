using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))] 
public class PlayerController : MonoBehaviour
{
    CharacterController _CharacterController;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraTransform;

    float playerWalkingSpeed = 6.0f;
    float playerRunningSpeed = 8.0f;
    float jumpHeight = 1.0f;
    float gravityValue = -9.81f;
    float rotationSpeed = 10f;
    float groundedTime;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    Animator animatorController;
    Vector2 animMove;

    bool IsRunning;
    public bool CanMove;
    public bool SwordEquiped;
    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        animatorController = GetComponentInChildren<Animator>();

        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        playerRunningSpeed = PlayerStats.instance.runningSpeed;
        playerWalkingSpeed = PlayerStats.instance.speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CanMove = true;
        SwordEquiped = false;
        groundedTime = 2f;
    }
    void Update()
    {
        if(CanMove != false)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && IsRunning != true)
                IsRunning = true;
            else if (Input.GetKeyDown(KeyCode.LeftControl) && IsRunning != false)
                IsRunning = false;

            groundedPlayer = _CharacterController.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0;
            }

            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;

            if (IsRunning != true)
                _CharacterController.Move(move * Time.deltaTime * playerWalkingSpeed);
            else
                _CharacterController.Move(move * Time.deltaTime * playerRunningSpeed);

            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (groundedPlayer)
            {
                if(IsRunning != true)
                {
                    animMove = new Vector2(horizontal, vertical);
                    animatorController.SetFloat("SpeedMultiplier", 1f);
                }    
                else
                {
                    animMove = new Vector2(horizontal, vertical);
                    animatorController.SetFloat("SpeedMultiplier", 1.3f);
                }                   
            }

            animatorController.SetBool("IsGrounded", groundedPlayer);
            animatorController.SetBool("IsRunning", IsRunning);

            if (groundedPlayer != false)
                groundedTime += Time.deltaTime;
            else
                groundedTime = 0f;

            if (jumpAction.triggered && groundedPlayer && groundedTime >= 2f)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animatorController.SetTrigger("Jump");
            }


            animatorController.SetFloat("X", animMove.x, 0.1f, Time.deltaTime);
            animatorController.SetFloat("Y", animMove.y, 0.1f, Time.deltaTime);

            playerVelocity.y += gravityValue * Time.deltaTime;
            _CharacterController.Move(playerVelocity * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animatorController.SetTrigger("EquipSword");
            CanMove = false;
            Invoke(nameof(SwordEquiping), 1.5f);
        }
    }
    void SwordEquiping()
    {
        CanMove = true;
        animatorController.SetInteger("State", 1);

        if (SwordEquiped != true)
            SwordEquiped = true;
        else 
            SwordEquiped = false;
    }
}