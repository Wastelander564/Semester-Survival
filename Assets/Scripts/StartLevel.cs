
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    private void Start()
    {
        // Activate the persistent gameplay UI
        if (GameplayUI.Instance != null)
        {
            GameplayUI.Instance.gameObject.SetActive(true);

            Debug.Log("Gameplay UI activated.");
        }
        else
        {
            Debug.LogError("GameplayUI Instance was not found!");
        }

        // Find and reset the timer
        timerScript timer = FindFirstObjectByType<timerScript>();

        if (timer != null)
        {
            timer.ResetTimer();

            Debug.Log("Timer reset for the new level.");
        }
        else
        {
            Debug.LogError("timerScript could not be found!");
        }
    }
}
