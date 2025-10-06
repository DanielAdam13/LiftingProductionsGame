using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private float cameraSensitivity = 0.2f;
    [SerializeField]
    private Vector2 cameraClampRangeX = new(-90f, 60f); // vertical
    [SerializeField]
    private Vector2 cameraClampRangeY = new(-120f, 120f); // horizontal

    [Header("Player Root")]
    [SerializeField]
    private Transform playerRoot;

    [Tooltip("Reference to the Move action from the Input Actions asset.")]
    [Header("Input Action Reference")]
    [SerializeField]
    private InputActionReference rotationActionReference;

    [Header("Script Reference")]
    [SerializeField]
    private PlayerBehaviourManager playerBehaviourManagerReference;

    // Non-assignable variables
    private Vector2 rotationInput;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private bool cameraLookingLocked;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        rotationActionReference.action.Enable();
    }

    private void OnDisable()
    {
        rotationActionReference.action.Disable();
    }

    private void Update()
    {
        if (!cameraLookingLocked)
        {
            rotationInput = rotationActionReference.action.ReadValue<Vector2>() * cameraSensitivity;

            xRotation -= rotationInput.y;
            xRotation = Mathf.Clamp(xRotation, cameraClampRangeX.x, cameraClampRangeX.y);

            yRotation += rotationInput.x;
            if (playerBehaviourManagerReference.GetCurrentState() == PlayerState.DrivingForklift)
            {
                yRotation = Mathf.Clamp(yRotation, cameraClampRangeY.x, cameraClampRangeY.y);
            }

            // UP/DOWN - rotate camera 
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // LEFT/RIGHT - rotate player root
            playerRoot.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }

    }

    public void LockOrUnlockCamera()
    {
        cameraLookingLocked = !cameraLookingLocked;
    }
}
