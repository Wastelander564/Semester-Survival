
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

    // Questionnaire GameObject
    public GameObject questionair;

    // GameManager
    public GameManager gameManager;

    // Base points for answering a question correctly
    public int CorrectScore = 100;

    // Points lost for every wrong answer
    public int NegativePoints = 25;

    // Number of wrong answers on the current question
    private int wrongAnswers = 0;

    // Prevents answering after the correct answer
    private bool answerSelected = false;

    // Keeps track of which answer buttons have already been clicked
    private HashSet<GameObject> clickedButtons =
        new HashSet<GameObject>();

    // Keeps a copy of all original questions
    private List<QuestionsAndAnswers> originalQnA =
        new List<QuestionsAndAnswers>();


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Make a copy of the original question pool
        if (QnA != null)
        {
            originalQnA =
                new List<QuestionsAndAnswers>(QnA);
        }
    }


    private void Start()
    {
        GenerateQuestion();
    }


    public void AnswerSelected(
        bool isCorrect,
        GameObject selectedButton
    )
    {
        // Make sure a valid button was supplied
        if (selectedButton == null)
        {
            Debug.LogError(
                "AnswerSelected received a null button!"
            );

            return;
        }

        // If the correct answer was already selected,
        // do not allow any more answers
        if (answerSelected)
        {
            return;
        }

        // Check if THIS specific button has already been clicked
        if (clickedButtons.Contains(selectedButton))
        {
            Debug.Log(
                "This answer has already been selected. " +
                "No additional points will be lost."
            );

            return;
        }

        // Mark this button as clicked
        clickedButtons.Add(selectedButton);

        // Get the Button component
        Button button =
            selectedButton.GetComponent<Button>();

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

                // Calculate the score based on wrong answers
                int pointsToGive =
                    CalculateCorrectScore();

                // Give points through GameManager
                if (gameManager != null)
                {
                    gameManager.UpdateScore(pointsToGive);
                }
                else
                {
                    Debug.LogError(
                        "GameManager is not assigned!"
                    );
                }

                Debug.Log(
                    "Correct answer! +" +
                    pointsToGive +
                    " points. Wrong answers: " +
                    wrongAnswers
                );

                // Prevent another answer
                answerSelected = true;

                // Remove the question that was just answered
                RemoveCurrentQuestion();

                // Close the questionnaire
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
                    Debug.LogError(
                        "GameManager is not assigned!"
                    );
                }

                Debug.Log(
                    "Wrong answer! -" +
                    NegativePoints +
                    " points. Wrong answers: " +
                    wrongAnswers
                );

                // Do NOT close the questionnaire.
                // Player can select another answer.

                // The same button cannot be clicked again
                // because it is now stored in clickedButtons.
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


    private void RemoveCurrentQuestion()
    {
        if (QnA == null || QnA.Count == 0)
        {
            return;
        }

        // Remove the question that was just answered
        QnA.RemoveAt(currentQuestion);

        Debug.Log(
            "Question removed. Questions remaining: " +
            QnA.Count
        );

        // If all questions have been used,
        // refill the pool with the original questions
        if (QnA.Count == 0)
        {
            Debug.Log(
                "All questions used. Resetting question pool."
            );

            QnA =
                new List<QuestionsAndAnswers>(
                    originalQnA
                );
        }
    }


    private IEnumerator CloseQuestionnaire()
    {
        // Give the player time to see the correct answer
        yield return new WaitForSeconds(2f);

        if (questionair != null)
        {
            questionair.SetActive(false);

            Debug.Log(
                "Questionnaire closed."
            );
        }
        else
        {
            Debug.LogError(
                "Questionnaire is not assigned!"
            );
        }

        // Prepare the next question
        GenerateQuestion();
    }


    private void SetAnswers()
    {
        if (QnA == null || QnA.Count == 0)
        {
            Debug.LogError(
                "QnA is empty!"
            );

            return;
        }

        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] == null)
            {
                Debug.LogError(
                    "Option " + i +
                    " is empty!"
                );

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

            // Reset answer
            answerScript.isCorrect = false;

            // Reset button color
            Button button =
                options[i].GetComponent<Button>();

            if (button != null)
            {
                ColorBlock colors =
                    button.colors;

                colors.selectedColor =
                    Color.white;

                button.colors = colors;

                // Make sure the button is interactable
                button.interactable = true;
            }

            // If there aren't enough answers,
            // clear this button
            if (
                i >=
                QnA[currentQuestion]
                .Answers.Length
            )
            {
                answerText.text = "";
                continue;
            }

            // Set answer text
            answerText.text =
                QnA[currentQuestion]
                .Answers[i];

            // Set correct answer
            if (
                QnA[currentQuestion]
                .CorrectAnswer == i + 1
            )
            {
                answerScript.isCorrect = true;
            }
        }

        // Clear the list of buttons clicked
        // for the previous question
        clickedButtons.Clear();

        // Allow a new answer
        answerSelected = false;
    }


    private void GenerateQuestion()
    {
        if (QnA == null || QnA.Count == 0)
        {
            Debug.LogError(
                "No questions available!"
            );

            return;
        }

        // Reset wrong answers for the new question
        wrongAnswers = 0;

        // Pick a random question
        currentQuestion =
            Random.Range(
                0,
                QnA.Count
            );

        // Display question
        if (QuestionTxt != null)
        {
            QuestionTxt.text =
                QnA[currentQuestion]
                .Question;
        }

        // Set answers
        SetAnswers();

        Debug.Log(
            "New question generated. Questions remaining: " +
            QnA.Count
        );
    }
}

