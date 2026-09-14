
using UnityEngine;

public class WeekScript : MonoBehaviour
{
    public GameManager gameManager;

    private bool weekUpdated = false;

    void Update()
    {
        teacherScript[] teachers = FindObjectsByType<teacherScript>(FindObjectsSortMode.None);

        if (teachers.Length == 0 && !weekUpdated)
        {
            weekUpdated = true;
            gameManager.UpdateWeek(1);
        }
    }
}

