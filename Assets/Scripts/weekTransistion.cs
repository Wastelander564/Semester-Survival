using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class WeekTransition : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI weekText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI WinText;
    public CanvasGroup canvasGroup;

    public int score;
    public int winScore;


    public float fadeDuration = 1f;
    public float waitTime = 1f;

    private void Start()
    {
        // Find the GameManager if it wasn't assigned manually
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManager could not be found!");
            return;
        }

        StartCoroutine(PlayWeekTransition());
    }

    private IEnumerator PlayWeekTransition()
    {
        ScoreText.text = "StudiePunten: " + gameManager.playerScore;
        WinText.text = "Needed StudiePunten: " + gameManager.winScore;
        // Show the current week
        weekText.text = "Week " + gameManager.Week;

        // Fade in
        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(waitTime);

        // Increase the week
        gameManager.UpdateWeek(1);

        // Show the new week
        weekText.text = "Week " + gameManager.Week;

        yield return new WaitForSeconds(waitTime);

        // Fade out
        yield return StartCoroutine(Fade(1f, 0f));

        // Load the scene matching the new week
        string nextSceneName = "Week " + gameManager.Week;

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                time / fadeDuration
            );

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
