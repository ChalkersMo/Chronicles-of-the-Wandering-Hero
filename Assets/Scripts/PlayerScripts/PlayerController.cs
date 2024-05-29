using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput), typeof(PlayerAudio))] 
public class PlayerController : MonoBehaviour, IEquipSword, IRunning
{
    [HideInInspector] public bool CanMove;
    public bool IsRunning { get; set; }
    public bool SwordEquiped { get; set; }

    public int numberOfCklicks = 0;

    public float WalkingSpeed { get; set; }
    public float RunningSpeed { get; set; }
    public float cooldownTime = 1.2f;

    public Vector3 CheckPointPosition;

    private CharacterController _CharacterController;
    private PlayerAnimController playerAnimController;
    private PlayerSwordController playerSwordController;
    private PlayerAudio playerAudio;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private float lastClickedTime = 0;
    private float maxComboDelay = 1;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float rotationSpeed = 10f;
    private float groundedTime;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool _canJump = true;
    private bool _canRoll = true;
   
    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        playerAnimController = GetComponent<PlayerAnimController>();
        playerAudio = GetComponent<PlayerAudio>();

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
        groundedPlayer = _CharacterController.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0;

        playerVelocity.y += gravityValue * Time.deltaTime;
        _CharacterController.Move(playerVelocity * Time.deltaTime);


        if (CanMove)
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
                playerAudio.RenewSource();
                playerAudio.PlayJumpSound();
                Invoke(nameof(JumpRenew), 1);
            }

            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;

            if (IsRunning)
            {
                playerAnimController.RunAnim();
                _CharacterController.Move(RunningSpeed * Time.deltaTime * move);

                if (move != Vector3.zero
               && playerAudio.audioSource.clip != playerAudio.runningClip
               && _canJump)
                {
                    playerAudio.PlayRunningSound();
                }
                else if (move == Vector3.zero && playerAudio.audioSource.clip == playerAudio.runningClip)
                {
                    playerAudio.RenewSource();
                }
            }
            else
            {
                playerAnimController.WalkAnim();
                _CharacterController.Move(WalkingSpeed * Time.deltaTime * move);

                if (move != Vector3.zero
               && playerAudio.audioSource.clip != playerAudio.walkingClip
               && _canJump)
                {
                    playerAudio.PlayWalkingSound();
                }
                else if (move == Vector3.zero && playerAudio.audioSource.clip == playerAudio.walkingClip)
                {
                    playerAudio.RenewSource();
                }
            }
         
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
        else
        {
            if (playerAudio.audioSource.clip == playerAudio.runningClip ||
                playerAudio.audioSource.clip == playerAudio.walkingClip)
            {
                playerAudio.RenewSource();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartAttacking();
        }
        if (Input.GetKeyDown(KeyCode.Q) && _canRoll)
        {
            playerAnimController.RollAnim();
            StartCoroutine(Roll(1));
            playerAudio.RenewSource();
            playerAudio.PlayRollSound();
        }
    }
    private void JumpRenew()
    {
        _canJump = true;
    }
    private void RollRenew()
    {
        _canRoll = true;
    }
    private IEnumerator Roll(float Duration)
    {
        float elapsed = 0.0f;

        CanMove = false;
        _canRoll = false;

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y + 1f);

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        while (elapsed < Duration)
        {
            _CharacterController.Move(RunningSpeed * Time.deltaTime * move);
            elapsed += Time.deltaTime;
            yield return null;
        }
        CanMove = true;
        Invoke(nameof(RollRenew), Duration);
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
    public void SavePosition(Vector3 position)
    {
        CheckPointPosition = position;
    }
    private void StartAttacking()
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

        playerAudio.RenewSource();
        playerAudio.PlaySlashSound();
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