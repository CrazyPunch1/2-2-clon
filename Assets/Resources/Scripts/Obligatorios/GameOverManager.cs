using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public Button retryButton;  // Optional if you have a retry button
    public Button mainMenuButton;  // Optional if you have a main menu button

    void Start()
    {
        gameOverUI.SetActive(false);  // Ensure the Game Over UI is hidden at the start

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RestartGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(() => LoadScene("MainMenu"));  // Replace "MainMenu" with your main menu scene name
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;  // Resume the game
        SceneManager.LoadScene(sceneName);
    }
}
