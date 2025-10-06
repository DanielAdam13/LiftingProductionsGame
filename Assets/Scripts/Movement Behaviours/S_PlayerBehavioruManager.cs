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

    [Tooltip("Reference to the Mount action from the Input Actions asset.")]
    [Header("Input Action Reference")]
    [SerializeField]
    private InputActionReference mountActionReference;
    [Tooltip("Reference to the Fork movement action from the Input Actions asset.")]
    [SerializeField]
    private InputActionReference forkActionReference;

    // Non-assignable variables
    private PlayerState currentState;

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
            Debug.Log("Pressed F");
            switch (currentState)
            {
                case PlayerState.Walking: // Entering forklift
                    if (IsInMountRange())
                    {
                        currentState = PlayerState.DrivingForklift;
                        walkingControllerReference.enabled = false;
                        forkliftControllerReference.enabled = true;

                        //Debug.Log("Switched to DrivingForklift state");
                    }
                    break;
                case PlayerState.DrivingForklift: // Leaving forklift

                    currentState = PlayerState.Walking;
                    forkliftControllerReference.enabled = false;
                    walkingControllerReference.enabled = true;

                    walkingControllerReference.TeleportPlayer(new Vector3(forkliftControllerReference.transform.position.x,
                        0f, forkliftControllerReference.transform.position.z) - (forkliftControllerReference.transform.right * 2.5f));

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

    private bool IsInMountRange()
    {
        return mountRangeCollider.bounds.Contains(transform.position);
    }

    public bool IsDrivingForklift()
    {
        return currentState == PlayerState.DrivingForklift;
    }


}
