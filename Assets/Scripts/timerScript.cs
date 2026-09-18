using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerScript : MonoBehaviour
{
    public float timeRemaining = 60f;
    public float startingTime = 60f;

    public GameObject timer;

    private RectTransform timerRect;
    private float startingWidth;
    private bool timerFinished = false;

    private void Awake()
    {
        // Make sure the timer survives scene changes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetupTimer();
        ResetTimer();
    }

    private void SetupTimer()
    {
        if (timer != null)
        {
            timer.SetActive(true);

            timerRect = timer.GetComponent<RectTransform>();

            startingWidth = 300f;
        }
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            // Count down
            timeRemaining -= Time.deltaTime;

            // Prevent the timer from going below 0
            timeRemaining = Mathf.Max(timeRemaining, 0f);

            // Calculate how much of the timer should remain
            float percentage = timeRemaining / startingTime;

            // Reduce the width
            if (timerRect != null)
            {
                timerRect.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    startingWidth * percentage
                );
            }
        }
        else if (!timerFinished)
        {
            timerFinished = true;

            // Make sure it reaches exactly 0
            timeRemaining = 0f;

            if (timerRect != null)
            {
                timerRect.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    0f
                );
            }

            // Go to the week transition
            SceneManager.LoadScene("WeekUpdateScene");
        }
    }

    public void ResetTimer()
    {
        timeRemaining = startingTime;
        timerFinished = false;

        // Find the timer again in case the UI was recreated
        if (timer == null)
        {
            timer = GameObject.Find("timer");
        }

        if (timer != null)
        {
            timer.SetActive(true);

            timerRect = timer.GetComponent<RectTransform>();

            if (timerRect != null)
            {
                startingWidth = 300f;

                timerRect.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    startingWidth
                );
            }
        }

        Debug.Log("Timer reset to " + startingTime + " seconds.");
    }
}
