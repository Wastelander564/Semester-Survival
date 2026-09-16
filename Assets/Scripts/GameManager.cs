using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0;
    public int Week = 1;
    public int winScore = 100000;

    public TMP_Text scoreText;
    public TMP_Text weekText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateScoreText();
        UpdateWeekText();
    }

    public void UpdateScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        UpdateScoreText();
    }

    public void UpdateWeek(int weekToAdd)
    {
        Week += weekToAdd;
        UpdateWeekText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore;
        }
    }

    private void UpdateWeekText()
    {
        if (weekText != null)
        {
            weekText.text = "Week: " + Week;
        }
    }
}