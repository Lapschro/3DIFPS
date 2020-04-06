using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPlayer : MonoBehaviour
{
    [Header("Player")]
    public float movementVelocity;
    public float mouseSensibility = 3;
    bool shootWeapon;
    public bool moving;
    public int id;
    public int score;

    Vector3 velocity;
    float cameraRotation = 0f;

    [Header("Constants")]
    public float gravity = -9.81f;

    InputMaster input;
    Vector2 mouseMovement;
    Vector2 movementDirection;
    Vector3 move;


    [Header("External Objects")]
    public Transform cameraTransform;
    public Transform weaponTransform;
    public CharacterController controller;
    public Transform groundCheck;
    public Weapon playerWeapon;

    [Header("Physics Configs")]
    public float groundDistance = 0.2f;
    public float jumpHeight = 1;
    public LayerMask groundMask;

    public ParticleSystem particles;

    bool isGrounded;
    bool isControllable;

    //Music
    private float stepTime = 0.5f;
    private float stepTimer = 0f;
    private int eventIndex = -1;
    protected CustomEventEmitter eventEmitter;

    //PhotonView photonView; // no need if inheriting from MonoBehaviourPun

    private void Awake() {
        //Check in photon if isMine if it is, proceed, else leave
        //photonView = GetComponent<PhotonView>(); // no need if inheriting from MonoBehaviourPun

        // we also want to control the character if we are outside a game room
        // photonView.IsMine is only available when inside a room
        //isControllable = photonView.IsMine || !PhotonNetwork.InRoom;

        input = new InputMaster();
        input.Player.Movement.performed += (ctx) => {
            //if (isControllable) //if (photonView.IsMine)
                movementDirection = ctx.ReadValue<Vector2>();
        };
        input.Player.MouseAxis.performed += (ctx) => {
            //if (isControllable) //if (photonView.IsMine)
                mouseMovement = ctx.ReadValue<Vector2>();

        };
        input.Player.MouseAxis.canceled += (_) => {
            //if (isControllable) //if (photonView.IsMine)
                mouseMovement = Vector2.zero;
        };

        input.Player.Jump.performed += (_) => {
            //if (isControllable) //if (photonView.IsMine)
                if (isGrounded) {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
        };

        input.Player.Fire.performed += (_) => {
            //
            //if (isControllable) //if (photonView.IsMine)
                shootWeapon = true;
        };

        input.Player.Fire.canceled += (_) => {
            //if (isControllable) //if (photonView.IsMine)
                shootWeapon = false;
        };
    }

    public void OnEnable() {
        input.Enable();
    }

    public void OnDisable() {
        input.Disable();
    }

    // Start is called before the first frame update
    void Start() {

        eventEmitter = CustomEventEmitter.instance;

        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();

        eventIndex = eventEmitter.StartEventThatFollows(FMODEvents.events[(int)FMODEvents.Player.WALK], gameObject, GetComponentInChildren<Rigidbody>());
    }

    // Update is called once per frame
    void Update() {
        eventEmitter.Set3DAttributes(eventIndex, gameObject);
        if (playerWeapon && shootWeapon) {
            playerWeapon.Shoot(cameraTransform.position, cameraTransform.forward);
            Debug.Log("Other Pew");
        }


        if (!moving || !isGrounded) {
            ParticleSystem.EmissionModule em = particles.emission;
            em.enabled = false;
        }
        else {
            ParticleSystem.EmissionModule em = particles.emission;
            em.enabled = true;
        }
    }

    private void FixedUpdate() {

        float dt = Time.fixedDeltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (mouseMovement.magnitude > 0.1f) {
            transform.Rotate(Vector3.up * mouseMovement.x * mouseSensibility * dt);


            cameraRotation = Mathf.Clamp(cameraRotation - mouseMovement.y * mouseSensibility * dt, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);
        }

        move = transform.right * movementDirection.x + transform.forward * movementDirection.y;

        controller.Move(move * movementVelocity * dt);

        moving = false;

        if (move.magnitude != 0) {
            moving = true;


            if (isGrounded) {
                //Simular atraso dos passos
                stepTimer += Time.deltaTime;
                if (stepTimer >= stepTime) {
                    eventEmitter.PlayEventInstance(eventIndex);
                    stepTimer = 0f;
                }
            }
            else {
                //Forçar som de passo na volta do pulo
                stepTimer = 0.2f;
            }
        }


        velocity.y += gravity * dt;

        controller.Move(velocity * dt);
    }


    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
