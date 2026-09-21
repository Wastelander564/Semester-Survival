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

    public GameObject transitionPanel;
    public GameObject GamePlayUI;

    public CanvasGroup canvasGroup;

    public float fadeDuration = 1f;
    public float waitTime = 1f;

    private void Start()
    {
        FindPersistentReferences();

        if (GamePlayUI != null)
        {
            GamePlayUI.SetActive(false);
        }

        if (gameManager == null || transitionPanel == null || canvasGroup == null)
        {
            Debug.LogError("WeekTransition could not find all required persistent references.");
            return;
        }

        StartCoroutine(PlayWeekTransition());
    }

    private void FindPersistentReferences()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        }

        if (GamePlayUI == null && GameplayUI.Instance != null)
        {
            GamePlayUI = GameplayUI.Instance.gameObject;
        }

        if (transitionPanel == null)
        {
            transitionPanel = GameObject.Find("transitionPanel");

            if (transitionPanel == null)
            {
                transitionPanel = GameObject.Find("TransitionPanel");
            }
        }

        if (canvasGroup == null)
        {
            canvasGroup = FindFirstObjectByType<CanvasGroup>(FindObjectsInactive.Include);
        }

        if (transitionPanel == null && canvasGroup != null)
        {
            transitionPanel = canvasGroup.gameObject;
        }
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
        if (gameManager.Week == gameManager.endWeek)
        {
            weekText.text = "Week " +gameManager.Week + " (Final Week)";
            weekText.fontSize = 225; // Increase font size for emphasis
        }

        // Update score text
        ScoreText.text = "StudiePunten: " + gameManager.playerScore;

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
        if (gameManager.Week > gameManager.endWeek)
        {
            weekText.text = "";
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