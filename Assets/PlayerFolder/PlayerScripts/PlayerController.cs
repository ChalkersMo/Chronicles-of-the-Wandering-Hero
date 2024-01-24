using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))] 
public class PlayerController : MonoBehaviour
{
    CharacterController _CharacterController;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraTransform;

    float playerSpeed = 2.0f;
    float jumpHeight = 1.0f;
    float gravityValue = -9.81f;
    float rotationSpeed = 5f;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        groundedPlayer = _CharacterController.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0) 
        { 
            playerVelocity.y = 0;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        _CharacterController.Move(move * Time.deltaTime * playerSpeed);

        if(jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); ;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _CharacterController.Move(playerVelocity * Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}