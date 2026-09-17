using UnityEngine;
using UnityEngine.SceneManagement;
using ClearSky;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private DemoCollegeStudentController playerController;
    [SerializeField] private string menuSceneName = "Menu";

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // No pausing when the level is already finished
            if (levelCompletePanel.activeSelf)
            {
                return;
            }

            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        playerController.enabled = false;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        playerController.enabled = true;
        isPaused = false;
    }

    public void Restart()
    {
        // Always unfreeze time before loading a scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        // Always unfreeze time before loading a scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}