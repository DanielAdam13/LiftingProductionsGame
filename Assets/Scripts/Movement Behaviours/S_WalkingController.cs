    using Unity.Android.Gradle.Manifest;
    using UnityEngine;
    using UnityEngine.InputSystem;

    // This component is automatically added when the PlayerController script is attached.
    [RequireComponent(typeof(CharacterController))]
    public class WalkingController : MonoBehaviour
    {
        [Header("Transform References")]
        [SerializeField]
        private Transform cameraTransform, forkliftTransform;

        [Space(8)]
        [SerializeField]
        private float walkingMoveSpeed = 6f;

        [Header("Input Action Reference")]
        [SerializeField]
        public InputActionReference playerMoveActionReference;  // ENABLE AND DISABLE IN MANAGER

        // Non-assignable variables
        private CharacterController characterController;
        private Vector2 moveInput;
        private Vector3 enterForkliftLocation;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            playerMoveActionReference.action.Enable();
            transform.parent = null;
            characterController.detectCollisions = true;
        }
        private void OnDisable()
        {
            playerMoveActionReference.action.Disable();

        if (forkliftTransform != null)
        {
            transform.parent = forkliftTransform;
            characterController.detectCollisions = false;
            this.transform.position = new Vector3(forkliftTransform.position.x - 0.1f, forkliftTransform.position.y - 0.2f, forkliftTransform.position.z);
            Debug.Log("Parent switched to forklift");
        }
        else
        {
            transform.parent = null;
            characterController.detectCollisions = false;
        }

    }

        // Update is called once per frame
        void Update()
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
