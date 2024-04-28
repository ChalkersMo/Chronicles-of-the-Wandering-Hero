using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))] 
public class PlayerController : MonoBehaviour, IEquipSword, IRunning
{
    private CharacterController _CharacterController;
    private PlayerAnimController playerAnimController;
    private PlayerSwordController playerSwordController;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    public int numberOfCklicks = 0;

    public float WalkingSpeed { get; set; }
    public float RunningSpeed { get; set; }
    public float cooldownTime = 1.2f;

    private float lastClickedTime = 0;
    private float maxComboDelay = 1;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 10f;
    private float groundedTime;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    [HideInInspector] public bool CanMove;
    public bool IsRunning { get; set; }
    public bool SwordEquiped { get; set; }

    private bool _canJump = true;

    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        playerAnimController = GetComponent<PlayerAnimController>();

        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        RunningSpeed = PlayerStats.instance.runningSpeed;
        WalkingSpeed = PlayerStats.instance.speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CanMove = true;
        SwordEquiped = false;
    }
    void Update()
    {
        if(CanMove)
        {
            playerAnimController.SetBoolsAnim(groundedPlayer, IsRunning);

            if (Input.GetKeyDown(KeyCode.LeftControl) && !IsRunning)
                IsRunning = true;
            else if (Input.GetKeyDown(KeyCode.LeftControl) && IsRunning)
                IsRunning = false;

            if (jumpAction.triggered && groundedPlayer && _canJump)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                playerAnimController.JumpAnim();
                _canJump = false;
                Invoke(nameof(JumpRenew), 2.5f);
            }

            groundedPlayer = _CharacterController.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
                playerVelocity.y = 0;


            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;

            if (IsRunning)
            {
                playerAnimController.RunAnim();
                _CharacterController.Move(RunningSpeed * Time.deltaTime * move);
            }
            else
            {
                playerAnimController.WalkAnim();
                _CharacterController.Move(WalkingSpeed * Time.deltaTime * move);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            _CharacterController.Move(playerVelocity * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if(Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            ComboRenewCheck();
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartAttacking();
        }
    }
    private void JumpRenew()
    {
        _canJump = true;
    }
    public void AssignSwordController(PlayerSwordController controller)
    {
        playerSwordController = controller;

        if (playerSwordController != null)
           playerAnimController.EquipSwordAnim();
        else
        {
            playerAnimController.SwordDisEquipingAnim();
        }
    }

    public void SwordEquiping()
    {
        CanMove = true;

        if (SwordEquiped != true)
        {
            playerAnimController.EquipSwordAnim();
            SwordEquiped = true;
        }                       
    }
    public void SwordDisEquiping()
    {
        CanMove = true;

        if (SwordEquiped != false)
        {
            playerAnimController.SwordDisEquipingAnim();
            SwordEquiped = false;
        }
    }

    void StartAttacking()
    {
        if (playerSwordController != null)
        {
            lastClickedTime = Time.time;
            numberOfCklicks++;
            numberOfCklicks = Mathf.Clamp(numberOfCklicks, 0, 3);

            if (SwordEquiped && CanMove)
            {
                playerAnimController.AttackAnim(numberOfCklicks);
                CanMove = false;
            }
        }
    }
    void ComboRenewCheck()
    {
        if (Time.time - lastClickedTime > maxComboDelay && playerSwordController != null)
        {
            numberOfCklicks = 0;
            playerAnimController.ComboRenewAnim();
            CanMove = true;
            playerSwordController.IsAttacking = false;
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
        playerAnimController.AfterAttackAnim();
        CanMove = true;
    }
    private void OnDisable()
    {
        playerAnimController.StandAnim();
    }
}