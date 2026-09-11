using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public float timeRemaining = 60f;
    public GameObject timer;

    private RectTransform timerRect;
    private float startingWidth;

    void Start()
    {
        if (timer != null)
        {
            timer.SetActive(true);

            // Get the RectTransform of the RawImage
            timerRect = timer.GetComponent<RectTransform>();

            // Remember its original width
            startingWidth = 300;
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
        else
        {
            // Make sure it reaches exactly 0
            timeRemaining = 0;

            timerRect.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                0
            );
        }
    }
}