using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private float cameraSensitivity = 0.2f;
    [SerializeField]
    public Vector2 cameraClampRangeX = new(-90f, 60f);
    [SerializeField]
    private Vector2 cameraClampRangeY = new(-120f, 120f);

    [Header("Player Root")]
    [SerializeField]
    private Transform playerRoot;

    [Header("Input Action Reference")]
    [Tooltip("Reference to the Move action from the Input Actions asset.")]
    public InputActionReference rotationActionReference;

    // Non-assignable variables
    private Vector2 rotationInput;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        rotationActionReference.action.Enable();
    }
    void OnDisable()
    {
        rotationActionReference.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        rotationInput = rotationActionReference.action.ReadValue<Vector2>() * cameraSensitivity;

        xRotation -= rotationInput.y;
        xRotation = Mathf.Clamp(xRotation, cameraClampRangeX.x, cameraClampRangeX.y);

        yRotation += rotationInput.x;
        yRotation = Mathf.Clamp(yRotation, cameraClampRangeY.x, cameraClampRangeY.y);


        // UP/DOWN - rotate camera 
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // LEFT/RIGHT - rotate player root
        playerRoot.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void LockAndUnlockCursor()
    {
        // pause, interactions with screens?
    }
}
