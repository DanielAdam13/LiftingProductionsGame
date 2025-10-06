using UnityEngine;
using UnityEngine.InputSystem;

// This component is automatically added when the PlayerController script is attached.
[RequireComponent(typeof(CharacterController))]
public class WalkingController : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform forkliftTransform;

    [Space(8)]
    [SerializeField]
    private float walkingMoveSpeed = 6f;

    [Header("Input Action Reference")]
    [SerializeField]
    public InputActionReference playerMoveActionReference;  // ENABLE AND DISABLE IN MANAGER

    // Non-assignable variables
    private CharacterController characterController;
    private Vector2 moveInput;
    private bool playerLocked;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        playerLocked = false;
    }

    private void OnEnable()
    {
        transform.parent = null;
        playerMoveActionReference.action.Enable();
        characterController.detectCollisions = true;
        characterController.enabled = true; // Ensure controller is enabled on walking start
    }

    private void OnDisable()
    {
        playerMoveActionReference.action.Disable();

        if (forkliftTransform != null)
        {
            transform.parent = forkliftTransform;
            characterController.detectCollisions = false;
            characterController.enabled = false; // Disable to prevent physics issues while parented

            transform.localPosition = new Vector3(-0.1f, -0.2f, 0); // Set local position relative to forklift
        }
        else
        {
            transform.parent = null;
            characterController.detectCollisions = false;
            characterController.enabled = false;
        }
    }

    private void Update()
    {
        if (!playerLocked)
        {
            moveInput = playerMoveActionReference.action.ReadValue<Vector2>();

            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 desiredMove = camForward * moveInput.y + camRight * moveInput.x;
            desiredMove = desiredMove.normalized;

            characterController.Move(walkingMoveSpeed * Time.deltaTime * desiredMove);
        }
    }

    public void TeleportPlayer(Vector3 pos)
    {
        transform.parent = null;
        characterController.enabled = false;
        transform.position = pos;
        characterController.enabled = true;
    }
    public void LockOrUnlockPlayer()
    {
        playerLocked = !playerLocked;
    }
}
