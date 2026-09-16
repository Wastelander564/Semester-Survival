using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerScript : MonoBehaviour
{
    public float timeRemaining = 60f;
    public GameObject timer;

    private RectTransform timerRect;
    private float startingWidth;
    private bool timerFinished = false;

    void Start()
    {
        if (timer != null)
        {
            timer.SetActive(true);

            // Get the RectTransform of the timer
            timerRect = timer.GetComponent<RectTransform>();

            // Remember its original width
            startingWidth = 300f;
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            // Count down
            timeRemaining -= Time.deltaTime;

            // Calculate how much of the timer should remain
            float percentage = timeRemaining / 60f;

            // Reduce the width
            timerRect.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                startingWidth * percentage
            );
        }
        else if (!timerFinished)
        {
            timerFinished = true;

            // Make sure it reaches exactly 0
            timeRemaining = 0;

            timerRect.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                0
            );

            // Go to the week transition
            SceneManager.LoadScene("WeekUpdateScene");
        }
    }
}