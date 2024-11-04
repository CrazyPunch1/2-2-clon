using UnityEngine;

public class MouseUnlocker : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.Escape; // Key to unlock/lock the cursor
    [SerializeField] private bool cursorInitiallyLocked = true;  // Set to true if you want the cursor locked at start

    private void Start()
    {
        SetCursorLock(cursorInitiallyLocked);
    }

    private void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Toggle cursor lock state
            bool isLocked = Cursor.lockState == CursorLockMode.Locked;
            SetCursorLock(!isLocked);
        }
    }

    private void SetCursorLock(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Optional method to unlock cursor externally
    public void UnlockCursor()
    {
        SetCursorLock(false);
    }

    // Optional method to lock cursor externally
    public void LockCursor()
    {
        SetCursorLock(true);
    }
}
