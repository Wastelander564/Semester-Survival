using UnityEngine;
using UnityEngine.SceneManagement;

public class WeekScript : MonoBehaviour
{
    public GameManager gameManager;

    private bool transitionStarted = false;

    void Update()
    {
        teacherScript[] teachers = FindObjectsByType<teacherScript>(FindObjectsSortMode.None);

        if (teachers.Length == 0 && !transitionStarted)
        {
            transitionStarted = true;

            SceneManager.LoadScene("WeekUpdateScene");
        }
    }
}