using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public GameObject questionaire;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");

            if (quizManager != null)
            {
                quizManager.correct();
            }

            if (questionaire != null)
            {
                questionaire.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
    }
}

