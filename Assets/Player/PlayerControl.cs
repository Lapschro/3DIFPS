using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player")]
    public float movementVelocity;
    public float mouseSensibility = 3;


    Vector3 velocity;
    float cameraRotation = 0f;

    [Header("Constants")]
    public float gravity = -9.81f;

    InputMaster input;
    Vector2 mouseMovement;
    Vector2 movementDirection;

    [Header("External Objects")]
    public Transform cameraTransform;
    public CharacterController controller;
    public Transform groundCheck;

    [Header("Physics Configs")]
    public float groundDistance = 0.2f;
    public float jumpHeight = 1;
    public LayerMask groundMask;

    bool isGrounded;

    private void Awake() {
        input = new InputMaster();
        input.Player.Movement.performed += (ctx) => {
            movementDirection = ctx.ReadValue<Vector2>();
        };
        input.Player.MouseAxis.performed += (ctx) => {
            mouseMovement = ctx.ReadValue<Vector2>();

        };
        input.Player.MouseAxis.canceled += (_) => {
            mouseMovement = Vector2.zero;
        };

        input.Player.Jump.performed += (_) => {
            if (isGrounded) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        };
    }

    private void OnEnable() {
        input.Enable();
    }

    private void OnDisable() {
        input.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        float dt = Time.fixedDeltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if(mouseMovement.magnitude > 0.1f) {
            transform.Rotate(Vector3.up * mouseMovement.x * mouseSensibility * dt);

            cameraRotation = Mathf.Clamp(cameraRotation - mouseMovement.y * mouseSensibility * dt, -90f, 90f );

            cameraTransform.localRotation = Quaternion.Euler(cameraRotation,0f,0f);
        }


        Vector3 move = transform.right * movementDirection.x + transform.forward * movementDirection.y ;
        
        controller.Move(move * movementVelocity * dt);

        velocity.y += gravity * dt;

        controller.Move(velocity * dt);
    }


    private void OnDrawGizmos() {
        Gizmos.color = new Color(1,0,0);
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
