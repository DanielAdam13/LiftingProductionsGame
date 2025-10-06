using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private PlayerBehaviourManager playerBehaviourManager;
    [SerializeField]
    private WalkingController walkingController;
    [SerializeField]
    private ForkliftController forkliftController;
    [SerializeField]
    private MouseLook mouseLook;

    [Header("State variables")]
    [SerializeField]
    private bool gamePaused;

    [Tooltip("Reference to the UI actions from the Input Actions asset.")]
    [Header("Input Action Reference")]
    [SerializeField]
    private InputActionReference pauseActionReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseActionReference.action.triggered)
        {
            gamePaused = !gamePaused;
            LockOrUnlockPlayerActions();
        }
    }

    private void LockOrUnlockPlayerActions()
    {
        walkingController.LockOrUnlockPlayer();
        forkliftController.LockOrUnlockForklift();
        mouseLook.LockOrUnlockCamera();
        uiManager.SwitchUIState();
        playerBehaviourManager.AllowOrDisallowSwitching();
    }
}
