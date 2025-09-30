using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Walking,
    DrivingForklift
}

public class PlayerBehaviourManager : MonoBehaviour
{
    [Tooltip("Reference to the Controller scripts from Forklift and Player.")]
    [Header("Script References")]
    [SerializeField]
    ForkliftController forkliftControllerReference;
    [SerializeField]
    WalkingController walkingControllerReference;

    [Space(5)]
    [SerializeField]
    private BoxCollider mountRangeCollider;

    [Tooltip("Reference to the Mount action from the Input Actions asset.")]
    [Header("Input Action Reference")]
    [SerializeField]
    private InputActionReference mountActionReference;

    // Non-assignable variables
    PlayerState currentState;
    private Vector3 relativeEntryPosition;

    private void Start()
    {
        currentState = PlayerState.Walking;
        //walkingControllerReference.enabled = true;
        forkliftControllerReference.enabled = false;
    }

    private void Update()
    {
        if (mountActionReference.action.triggered)
        {
            switch (currentState)
            {
                case PlayerState.Walking: // Entering forklift
                    if (IsInMountRange())
                    {
                        relativeEntryPosition = forkliftControllerReference.transform.InverseTransformPoint(walkingControllerReference.transform.position);
                        Debug.Log("ENTRY  " + relativeEntryPosition);

                        currentState = PlayerState.DrivingForklift;
                        walkingControllerReference.enabled = false;
                        forkliftControllerReference.enabled = true;

                        //Debug.Log("Switched to DrivingForklift state");
                    }
                    break;
                case PlayerState.DrivingForklift: // Leaving forklift
                    currentState = PlayerState.Walking;

                    forkliftControllerReference.enabled = false;

                    walkingControllerReference.transform.position = forkliftControllerReference.transform.TransformPoint(relativeEntryPosition);
                    walkingControllerReference.enabled = true;

                    

                    //Debug.Log("Switched to Walking state");
                    break;
            }
        }
    }

    private bool IsInMountRange()
    {
        return mountRangeCollider.bounds.Contains(transform.position);
    }
}
