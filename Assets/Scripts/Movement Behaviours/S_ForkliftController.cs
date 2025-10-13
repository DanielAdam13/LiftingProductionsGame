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

    private GameObject Fork;

    private Vector2 moveInput;

    private void Awake()
    {
        Fork = transform.Find("Fork").gameObject;
        if (Fork == null)
        {
            Debug.LogError("Fork object not found as a child of the forklift.");
        }
        else
        {
            Debug.Log("Fork object found: " + Fork.name);
        }
    }

    private void OnEnable()
    {
        forkliftMoveActionReference.action.Enable();
    }
    private void OnDisable()
    {
        forkliftMoveActionReference.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetFork().GetComponent<S_ForkPickUp>().TriggerEffect(other);
    }

    void Update()
    {
        moveInput = forkliftMoveActionReference.action.ReadValue<Vector2>();

        // Rotate the player based on horizontal input.
        transform.Rotate(0, moveInput.x * forkliftRotationSpeed * Time.deltaTime, 0);

        // moveDirection is based on camera direction
        Vector3 moveDirection = transform.forward * moveInput.y;

        transform.position += forkliftMoveSpeed * Time.deltaTime * moveDirection;
    }
    public ref GameObject GetFork() { return ref Fork; }
}
