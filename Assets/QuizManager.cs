using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public TMP_Text QuestionTxt;

    private void Start()
    {
        GenerateQuestion();
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
                    options[i].name + " is missing AnswerScript!"
                );
                continue;
            }

            if (options[i].transform.childCount == 0)
            {
                Debug.LogError(
                    options[i].name + " has no child!"
                );
                continue;
            }

            TMP_Text answerText =
                options[i].transform.GetChild(0).GetComponent<TMP_Text>();

            if (answerText == null)
            {
                Debug.LogError(
                    options[i].name + " is missing TMP_Text!"
                );
                continue;
            }

            answerScript.isCorrect = false;

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
    }

    private void GenerateQuestion()
    {
        if (QnA == null || QnA.Count == 0)
        {
            QuestionTxt.text = "Quiz Complete!";
            return;
        }

        currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text =
            QnA[currentQuestion].Question;

        SetAnswers();
    }
}
