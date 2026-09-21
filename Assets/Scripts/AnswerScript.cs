using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect;

    public QuizManager quizManager;

    public void Answer()
    {
        Debug.Log("ANSWER CLICKED: " + gameObject.name);

        if (quizManager == null)
        {
            Debug.LogError(
                "QuizManager is not assigned to " + gameObject.name
            );

            return;
        }

        quizManager.AnswerSelected(
            isCorrect,
            gameObject
        );
    }
}