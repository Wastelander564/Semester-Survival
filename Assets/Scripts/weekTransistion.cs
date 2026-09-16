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
        // Show current week
        weekText.text = "Week " + gameManager.Week;

        // Show current score
        ScoreText.text = "Score: " + gameManager.playerScore;

        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(waitTime);

        // Increase week
        gameManager.UpdateWeek(1);

        // Update week text
        weekText.text = "Week " + gameManager.Week;

        // Update score text
        ScoreText.text = "Score: " + gameManager.playerScore;

        // Show whether the player has enough study points
        if (gameManager.playerScore >= gameManager.winScore)
        {
            WinText.text = "Voldoende studiepunten";
        }
        else
        {
            WinText.text = "Onvoldoende studiepunten";
        }

        yield return new WaitForSeconds(waitTime);

        yield return StartCoroutine(Fade(1f, 0f));

        // Week 17 is finished, go to EndScreen
        if (gameManager.Week > 17)
        {
            SceneManager.LoadScene("EndScreen");
        }
        else
        {
            string nextSceneName = "Week " + gameManager.Week;
            SceneManager.LoadScene(nextSceneName);
        }
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