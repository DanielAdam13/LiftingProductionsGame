using UnityEngine;
using UnityEngine.InputSystem;

// This component is automatically added when the PlayerController script is attached.
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The speed at which the player moves.")]
    public float moveSpeed = 5.0f;

    [Tooltip("The speed at which the player rotates.")]
    public float RotationSpeed = 15.0f;

    [Tooltip("The strength of gravity applied to the player.")]
    public float gravityValue = -9.81f;

    [Header("Input Action References")]
    [Tooltip("Reference to the Move action from the Input Actions asset.")]
    public InputActionReference moveActionReference;

    // Private variables
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private Vector2 moveInput;

    private void Awake()
    {
        // Get the CharacterController component attached to this GameObject.
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        // Enable the input actions when the script is enabled.
        moveActionReference.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions when the script is disabled to prevent errors.
        moveActionReference.action.Disable();
    }

    void Update()
    {
        // Check if the character is on the ground.
        isGrounded = characterController.isGrounded;

        // If grounded, reset the vertical velocity. This prevents gravity from accumulating.
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Read the 2D vector input from the move action.
        moveInput = moveActionReference.action.ReadValue<Vector2>();

        // Rotate the player based on horizontal input.
        transform.Rotate(0, moveInput.x * RotationSpeed * Time.deltaTime, 0);

        // Create a 3D movement vector based on the camera's forward and right directions.
        Vector3 moveDirection =  transform.forward * moveInput.y;

        // Apply the movement using the CharacterController.
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Apply gravity to the player's vertical velocity.
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Apply the vertical velocity (for gravity).
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
