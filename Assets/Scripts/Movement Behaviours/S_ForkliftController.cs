using System;
using UnityEngine;
using UnityEngine.InputSystem;

// This component is automatically added when the PlayerController script is attached.
[RequireComponent(typeof(CharacterController))]
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
    private CharacterController forkliftCharacterController;
    private Vector2 moveInput;

    private void Awake()
    {
        // Get the CharacterController component attached to this GameObject.
        forkliftCharacterController = GetComponent<CharacterController>();
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
        moveInput = forkliftMoveActionReference.action.ReadValue<Vector2>();

        // Rotate the player based on horizontal input.
        transform.Rotate(0, moveInput.x * forkliftRotationSpeed * Time.deltaTime, 0);

        // moveDirection is based on camera direction
        Vector3 moveDirection = transform.forward * moveInput.y;

        forkliftCharacterController.Move(forkliftMoveSpeed * Time.deltaTime * moveDirection);
    }
}
