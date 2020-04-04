using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player")]
    public float movementVelocity;
    public float mouseSensibility = 3;
    bool shootWeapon;
    public bool moving;
    public int id;


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
    public Weapon playerWeapon;

    [Header("Physics Configs")]
    public float groundDistance = 0.2f;
    public float jumpHeight = 1;
    public LayerMask groundMask;

    bool isGrounded;

    private void Awake() {
        //Check in photon if isMine if it is, proceed, else leave

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

        input.Player.Fire.performed += (_) => {
            //
            shootWeapon = true;
        };

        input.Player.Fire.canceled += (_) => {
            shootWeapon = false;
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
        if (shootWeapon) {
            playerWeapon.Shoot(cameraTransform.position, cameraTransform.forward);
        }
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

        moving = false;

        if(move.magnitude != 0) {
            moving = true;
        }

        velocity.y += gravity * dt;

        controller.Move(velocity * dt);
    }


    private void OnDrawGizmos() {
        Gizmos.color = new Color(1,0,0);
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
