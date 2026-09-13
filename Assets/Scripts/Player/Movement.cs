using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    private Net myNet;

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 4.0f;
    // [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private float upDownRange = 80.0f;

    [Header("Input Actions")]
    [SerializeField] private InputActionAsset playerControls;

    private bool isMoving;
    private Camera mainCamera;
    private float verticalRotation;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private Vector2 moveInput;
    private Vector2 lookInput;

    public Inventory myInventory;


    private void Awake()
    {
        myNet = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Net>();
        myInventory = GetComponent<Inventory>();
        characterController = this.GetComponent<CharacterController>();
        mainCamera = Camera.main;

        moveAction = playerControls.FindActionMap("Player").FindAction("Move");
        lookAction = playerControls.FindActionMap("Player").FindAction("Look");
        jumpAction = playerControls.FindActionMap("Player").FindAction("Jump");

        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (myNet.canSwing && myInventory.isInventoryClosed)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    void HandleMovement()
    {
        // float speedMultiplier = sprintAction.ReadValue<float>() > 0 ? sprintMultiplier : 1f;

        float verticalSpeed = moveInput.y * walkSpeed;
        float horizontalSpeed = moveInput.x * walkSpeed;

        Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        HandleGravityAndJumping();

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        characterController.Move(currentMovement * Time.deltaTime);

        isMoving = moveInput.y != 0 || moveInput.x != 0;
    }

    void HandleGravityAndJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (jumpAction.triggered) currentMovement.y = jumpForce;
        }
        else currentMovement.y -= gravity * Time.deltaTime;
    }

    void HandleRotation()
    {
        float mouseXRotation = lookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= lookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
