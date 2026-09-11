using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;

    public int currentQuestion;

    public TMP_Text QuestionTxt;

    // Drag your questionnaire GameObject here
    public GameObject questionair;

    // Drag your GameManager here
    public GameManager gameManager;

    // Base points for answering a question correctly
    public int CorrectScore = 100;

    // Points lost for every wrong answer
    public int NegativePoints = 25;

    // Number of wrong answers on the current question
    private int wrongAnswers = 0;

    private bool answerSelected = false;

    private void Start()
    {
        GenerateQuestion();
    }

    public void AnswerSelected(bool isCorrect, GameObject selectedButton)
    {
        // Prevent clicking after the correct answer
        if (answerSelected)
            return;

        // Get the Button component
        Button button = selectedButton.GetComponent<Button>();

        if (button != null)
        {
            ColorBlock colors = button.colors;

            if (isCorrect)
            {
                // Correct = green
                colors.selectedColor = Color.green;
                button.colors = colors;

                // Select the button so the selected color is shown
                button.Select();

                // Calculate the score based on how many times
                // the player answered incorrectly first
                int pointsToGive = CalculateCorrectScore();

                // Give the points through GameManager
                if (gameManager != null)
                {
                    gameManager.UpdateScore(pointsToGive);
                }
                else
                {
                    Debug.LogError("GameManager is not assigned!");
                }

                Debug.Log(
                    "Correct answer! +" + pointsToGive +
                    " points. Wrong answers: " + wrongAnswers
                );

                // Only close the questionnaire if the answer is correct
                answerSelected = true;

                StartCoroutine(CloseQuestionnaire());
            }
            else
            {
                // Incorrect = red
                colors.selectedColor = Color.red;
                button.colors = colors;

                // Select the button so the selected color is shown
                button.Select();

                // Add one mistake for this question
                wrongAnswers++;

                // Take away points
                if (gameManager != null)
                {
                    gameManager.UpdateScore(-NegativePoints);
                }
                else
                {
                    Debug.LogError("GameManager is not assigned!");
                }

                Debug.Log(
                    "Wrong answer! -" + NegativePoints +
                    " points. Wrong answers: " + wrongAnswers
                );

                // DO NOT close the questionnaire
                // The player can try another answer
            }
        }
        else
        {
            Debug.LogError(
                selectedButton.name +
                " does not have a Button component!"
            );
        }
    }

    private int CalculateCorrectScore()
    {
        // Start with the normal score
        int points = CorrectScore;

        // Halve the points for every wrong answer
        for (int i = 0; i < wrongAnswers; i++)
        {
            points /= 2;
        }

        return points;
    }

    private IEnumerator CloseQuestionnaire()
    {
        yield return new WaitForSeconds(2f);

        if (questionair != null)
        {
            questionair.SetActive(false);
        }
        else
        {
            Debug.LogError("Questionair is not assigned!");
        }
    }

    public void correct()
    {
        if (QnA == null || QnA.Count == 0)
        {
            Debug.Log("No questions remaining.");
            return;
        }

        QnA.RemoveAt(currentQuestion);

        if (QnA.Count == 0)
        {
            QuestionTxt.text = "Quiz Complete!";
            return;
        }

        GenerateQuestion();
    }

    private void SetAnswers()
    {
        if (QnA == null || QnA.Count == 0)
        {
            Debug.LogError("QnA is empty!");
            return;
        }

        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] == null)
            {
                Debug.LogError("Option " + i + " is empty!");
                continue;
            }

            AnswerScript answerScript =
                options[i].GetComponent<AnswerScript>();

            if (answerScript == null)
            {
                Debug.LogError(
                    options[i].name +
                    " is missing AnswerScript!"
                );

                continue;
            }

            if (options[i].transform.childCount == 0)
            {
                Debug.LogError(
                    options[i].name +
                    " has no child!"
                );

                continue;
            }

            TMP_Text answerText =
                options[i]
                .transform
                .GetChild(0)
                .GetComponent<TMP_Text>();

            if (answerText == null)
            {
                Debug.LogError(
                    options[i].name +
                    " is missing TMP_Text!"
                );

                continue;
            }

            answerScript.isCorrect = false;

            // Reset button to normal state
            Button button =
                options[i].GetComponent<Button>();

            if (button != null)
            {
                ColorBlock colors = button.colors;

                colors.selectedColor = Color.white;

                button.colors = colors;
            }

            if (i >= QnA[currentQuestion].Answers.Length)
            {
                answerText.text = "";
                continue;
            }

            answerText.text =
                QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                answerScript.isCorrect = true;
            }
        }

        // Allow a new answer
        answerSelected = false;
    }

    private void GenerateQuestion()
    {
        if (QnA == null || QnA.Count == 0)
        {
            QuestionTxt.text = "Quiz Complete!";
            return;
        }

        // Reset wrong answers for the NEW question
        wrongAnswers = 0;

        currentQuestion =
            Random.Range(0, QnA.Count);

        QuestionTxt.text =
            QnA[currentQuestion].Question;

        SetAnswers();
    }
}
