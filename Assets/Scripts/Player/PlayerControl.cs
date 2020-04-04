﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

#pragma warning disable 649

public class PlayerControl : MonoBehaviour, IPunObservable
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
    public Transform weaponTransform;
    public CharacterController controller;
    public Transform groundCheck;
    public Weapon playerWeapon;

    [Header("Physics Configs")]
    public float groundDistance = 0.2f;
    public float jumpHeight = 1;
    public LayerMask groundMask;

    bool isGrounded;

    PhotonView photonView;

    private void Awake() {
        //Check in photon if isMine if it is, proceed, else leave
        photonView = GetComponent<PhotonView>();

        input = new InputMaster();
        input.Player.Movement.performed += (ctx) => {
            if(photonView.IsMine)
            movementDirection = ctx.ReadValue<Vector2>();
        };
        input.Player.MouseAxis.performed += (ctx) => {
            if (photonView.IsMine)
                mouseMovement = ctx.ReadValue<Vector2>();

        };
        input.Player.MouseAxis.canceled += (_) => {
            if (photonView.IsMine)
                mouseMovement = Vector2.zero;
        };

        input.Player.Jump.performed += (_) => {
            if (photonView.IsMine)
                if (isGrounded) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        };

        input.Player.Fire.performed += (_) => {
            //
            if (photonView.IsMine)
                shootWeapon = true;
        };

        input.Player.Fire.canceled += (_) => {
            if (photonView.IsMine)
                shootWeapon = false;
        };

        if (!photonView.IsMine) {
            GetComponentInChildren<Camera>().enabled = false;
        }
    }

    public  void OnEnable() {
        input.Enable();
    }

    public  void OnDisable() {
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
        if (!photonView.IsMine)
            return;
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


    //IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(this.shootWeapon);
            stream.SendNext(this.movementDirection);
            stream.SendNext(this.mouseMovement);
            stream.SendNext(this.mouseSensibility);
        }
        else {
            shootWeapon = (bool)stream.ReceiveNext();
            movementDirection = (Vector2)stream.ReceiveNext();
            mouseMovement = (Vector2)stream.ReceiveNext();
            mouseSensibility = (float)stream.ReceiveNext();
        }
    }

}
