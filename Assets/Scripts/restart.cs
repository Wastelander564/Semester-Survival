using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public void Restart()
    {
        // Find the persistent GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            // Reset all game progress
            gameManager.playerScore = 0;
            gameManager.Week = 1;

            // Update the UI
            if (gameManager.scoreText != null)
            {
                gameManager.scoreText.text = "StudiePunten: 0";
            }

            if (gameManager.weekText != null)
            {
                gameManager.weekText.text = "Week: 1";
            }

            Debug.Log("Game progress reset: Week 1, Score 0.");
        }
        else
        {
            Debug.LogWarning("GameManager could not be found.");
        }

        // Find the persistent timer
        timerScript timer = FindObjectOfType<timerScript>();

        if (timer != null)
        {
            // Reset timer back to its starting time
            timer.ResetTimer();

            Debug.Log("Timer reset.");
        }
        else
        {
            Debug.LogWarning("timerScript could not be found.");
        }

        // Go back to the Menu
        SceneManager.LoadScene("Menu");
    }
}