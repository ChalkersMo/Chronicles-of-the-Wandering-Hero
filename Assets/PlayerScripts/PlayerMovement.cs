using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController _CharacterController;
    Transform _cam;

    float _turnSmoothVelocity;
    float _turnSmoothTime = 0.1f;

    private void Start()
    {
        _CharacterController = GetComponent<CharacterController>();
        _cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            var speed = PlayerStats.instance.speed;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _CharacterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}