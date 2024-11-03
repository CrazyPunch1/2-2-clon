using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    static bool currentPauseState = false; // Tracks the pause state of the game
    [SerializeField] GameObject pauseMenu; // Reference to the pause menu UI

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!currentPauseState)
            {
                PauseGameTime();
            }
            else
            {
                UnPauseGameTime();
            }
        }
    }

    public void PauseGameTime()
    {
        currentPauseState = true; // Set pause state to true
        Time.timeScale = 0; // Stop game time
        pauseMenu.SetActive(true); // Show the pause menu
        UnlockCursor(); // Unlock and show the cursor
    }

    public void UnPauseGameTime()
    {
        currentPauseState = false; // Set pause state to false
        Time.timeScale = 1; // Resume game time
        pauseMenu.SetActive(false); // Hide the pause menu
        LockCursor(); // Lock and hide the cursor
    }

    // Restart the current scene, ensuring the game is unpaused before loading
    public void UnpauseAndRestart()
    {
        UnpauseAndLoad(SceneManager.GetActiveScene().name);
    }

    // Unpause the game and load a new scene
    public void UnpauseAndLoad(string nextScene)
    {
        currentPauseState = false; // Ensure the pause state is reset
        Time.timeScale = 1; // Ensure game time is resumed
        SceneManager.LoadScene(nextScene); // Load the specified scene
    }

    // Lock the cursor and hide it
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Unlock the cursor and make it visible
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
