using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForkliftController : MonoBehaviour
{
    [Header("Forklift Movement Settings")]
    [SerializeField]
    private float forkliftMoveSpeed = 5f;
    [SerializeField]
    private float forkliftRotationSpeed = 15f;

    [Header("Input Action References")]
    [Tooltip("Reference to the Move action from the Input Actions asset.")]
    [SerializeField]
    private InputActionReference forkliftMoveActionReference;

    // Non-assignable variables
    private Vector2 moveInput;
    private bool forkliftControllerLocked;

    private void Start()
    {
        forkliftControllerLocked = false;
    }

    private void OnEnable()
    {
        forkliftMoveActionReference.action.Enable();
    }
    private void OnDisable()
    {
        forkliftMoveActionReference.action.Disable();
    }

    void Update()
    {
        if (!forkliftControllerLocked)
        {
            moveInput = forkliftMoveActionReference.action.ReadValue<Vector2>();

            // Rotate the player based on horizontal input.
            transform.Rotate(0, moveInput.x * forkliftRotationSpeed * Time.deltaTime, 0);

            // moveDirection is based on camera direction
            Vector3 moveDirection = transform.forward * moveInput.y;

            transform.position += forkliftMoveSpeed * Time.deltaTime * moveDirection;
        }
    }
    public void LockOrUnlockForklift()
    {
        forkliftControllerLocked = !forkliftControllerLocked;
    }
}
