using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Gameobject references")]
    [SerializeField]
    private GameObject UI_pauseMenu;
    [SerializeField]
    private GameObject UI_mountText;
    [SerializeField]
    private GameObject UI_crosshair;

    [Header("Script Reference")]
    [SerializeField]
    private PlayerBehaviourManager playerBehaviourManagerReference;

    // Non-assignable variables
    private bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (playerBehaviourManagerReference.GetCurrentState() == PlayerState.Walking &&
                playerBehaviourManagerReference.IsInMountRange() &&
                !UI_mountText.activeSelf)
            {
                UI_mountText.SetActive(true);
            }
            else if (!playerBehaviourManagerReference.IsInMountRange() || playerBehaviourManagerReference.GetCurrentState() == PlayerState.DrivingForklift)
            { 
                UI_mountText.SetActive(false);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            UI_mountText.SetActive(false);
        }

        UI_pauseMenu.SetActive(isPaused);
        UI_crosshair.SetActive(!isPaused);
    }

    public void SwitchUIState()
    {
        isPaused = !isPaused;
    }
}
