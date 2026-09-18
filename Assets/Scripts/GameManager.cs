
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0;
    public int Week = 1;
    public int winScore = 100000;
    public int endWeek = 5;

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

    public void TeacherDestroyed()
    {
        Debug.Log("Teacher destroyed. Checking remaining teachers...");

        // Find all teachers that are still in the scene
        teacherScript[] remainingTeachers = FindObjectsOfType<teacherScript>();

        Debug.Log("Remaining teachers: " + remainingTeachers.Length);

        // If there are no teachers left, the level is complete
        if (remainingTeachers.Length == 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        Debug.Log("Level complete! Transitioning to the next week...");

        // Load the transition scene, which advances the week and loads the next level.
        SceneManager.LoadScene("WeekUpdateScene");
    }
}
