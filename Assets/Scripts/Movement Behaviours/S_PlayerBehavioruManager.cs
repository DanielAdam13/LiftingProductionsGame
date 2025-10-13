using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum PlayerState
{
    Walking,
    DrivingForklift
}

public class PlayerBehaviourManager : MonoBehaviour
{
    [Tooltip("Reference to the Controller scripts for Forklift and Player.")]
    [Header("Script References")]
    [SerializeField]
    ForkliftController forkliftControllerReference;
    [SerializeField]
    WalkingController walkingControllerReference;

    [Space(5)]
    [SerializeField]
    private CapsuleCollider mountRangeCollider;
    [SerializeField]
    [Tooltip("UI gameObject fro invalid exit")]
    private GameObject UI_invalidExitMessage;

    [Tooltip("Reference to the Mount action from the Input Actions asset.")]
    [Header("Input Action Reference")]
    [SerializeField]
    private InputActionReference mountActionReference;
    [Tooltip("Reference to the Fork movement action from the Input Actions asset.")]
    [SerializeField]
    private InputActionReference forkActionReference;

    // Non-assignable variables
    private PlayerState currentState;
    private bool allowedSwitch;
    private Vector3 dismountDirection;
    private Vector3 exitPostion;
    
    private void Start()
    {
        currentState = PlayerState.Walking;
        //walkingControllerReference.enabled = true;
        forkliftControllerReference.enabled = false;

        allowedSwitch = true;
    }

    private void Update()
    {
        Debug.DrawLine(forkliftControllerReference.transform.position, forkliftControllerReference.transform.position + dismountDirection * 10f, Color.green);
        if (mountActionReference.action.triggered && allowedSwitch)
        {
            switch (currentState)
            {
                case PlayerState.Walking: // Entering forklift
                    if (IsInMountRange())
                    {
                        currentState = PlayerState.DrivingForklift;
                        walkingControllerReference.enabled = false;
                        forkliftControllerReference.enabled = true;
                    }
                    break;
                case PlayerState.DrivingForklift: // Leaving forklift
                    exitPostion = Vector3.zero;

                    

                    dismountDirection = forkliftControllerReference.transform.right;

                    bool validLeft = ValidateDismountDirection(-forkliftControllerReference.transform.right);
                    bool validRight = ValidateDismountDirection(forkliftControllerReference.transform.right);

                    if (!validLeft && !validRight)
                    {
                        Debug.Log("Blocked on both sides!");

                        UI_invalidExitMessage.SetActive(true);
                        Invoke(nameof(DisableObject), 1.5f);

                        break;
                    }
                    else if (validLeft && !validRight)
                    {
                        dismountDirection = -forkliftControllerReference.transform.right;
                    }
                    else if (validRight && !validLeft)
                    {
                        
                    }

                    exitPostion = new Vector3(forkliftControllerReference.transform.position.x,
                        0f, forkliftControllerReference.transform.position.z) + (dismountDirection * 2.5f);

                    walkingControllerReference.TeleportPlayer(exitPostion);

                    currentState = PlayerState.Walking;
                    forkliftControllerReference.enabled = false;
                    walkingControllerReference.enabled = true;

                    break;
            }
        }

        if (forkActionReference.action.IsPressed() && currentState == PlayerState.DrivingForklift)
        {
            bool keyPressed = forkActionReference.action.ReadValue<float>() > 0;
            if (keyPressed)
            {
                //Debug.Log("Released P");
                forkliftControllerReference.GetFork().GetComponent<S_ForkPickUp>().MoveVerticalMovement(keyPressed);
            } else
            {
                //Debug.Log("Released O");
                forkliftControllerReference.GetFork().GetComponent<S_ForkPickUp>().MoveVerticalMovement(keyPressed);
            }
        }
    }

    public bool IsInMountRange()
    {
        return mountRangeCollider.bounds.Contains(transform.position);
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    public void AllowOrDisallowSwitching()
    {
        allowedSwitch = !allowedSwitch;
    }

    private bool ValidateDismountDirection(Vector3 checkDirection)
    {
        bool hitSomething = Physics.Raycast(forkliftControllerReference.transform.position, checkDirection, out RaycastHit hit, 3f);

        if (hitSomething)
        {
            //Debug.Log("Status: " + !hit.collider.CompareTag("Static Level Mesh"));
            return !hit.collider.CompareTag("Static Level Mesh");
        }
        return true;
    }

    private void DisableObject()
    {
        UI_invalidExitMessage.SetActive(false);
    }


}
