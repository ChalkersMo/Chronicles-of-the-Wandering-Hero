using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))] 
public class PlayerController : MonoBehaviour, IEquipSword, IRunning
{
    CharacterController _CharacterController;
    PlayerAnimController playerAnimController;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraTransform;

    public float WalkingSpeed { get; set; }
    public float RunningSpeed { get; set; }

    float jumpHeight = 1.0f;
    float gravityValue = -9.81f;
    float rotationSpeed = 10f;
    float groundedTime;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    public bool IsRunning { get; set; }
    [HideInInspector] public bool CanMove;
    public bool SwordEquiped { get; set; }

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
        groundedTime = 2f;
    }
    void Update()
    {
        if(CanMove != false)
        {
            playerAnimController.SetBools(groundedPlayer, IsRunning);

            if (Input.GetKeyDown(KeyCode.LeftControl) && !IsRunning)
                IsRunning = true;
            else if (Input.GetKeyDown(KeyCode.LeftControl) && IsRunning)
                IsRunning = false;

            if (jumpAction.triggered && groundedPlayer && groundedTime >= 1)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                playerAnimController.Jump();
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
                _CharacterController.Move(RunningSpeed * Time.deltaTime * move);
                playerAnimController.Run();
            }
            else
            {
                _CharacterController.Move(WalkingSpeed * Time.deltaTime * move);
                playerAnimController.Walk();
            }
            
            if (groundedPlayer != false)
                groundedTime += Time.deltaTime;
            else
                groundedTime = 0f;

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
        }
    }
   
    public void SwordEquiping()
    {
        CanMove = true;

        if (SwordEquiped != true)
        {
            playerAnimController.EquipSword();
            SwordEquiped = true;
        }                       
    }
    public void SwordDisEquiping()
    {
        CanMove = true;

        if (SwordEquiped != false)
        {
            playerAnimController.SwordDisEquiping();
            SwordEquiped = false;
        }
    }
}