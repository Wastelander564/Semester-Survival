using UnityEngine;
using UnityEngine.SceneManagement;
using ClearSky;

public class LevelFinish : MonoBehaviour
{
    // The "Week complete" panel. 
    [SerializeField] private GameObject levelCompletePanel;

    [SerializeField] private string menuSceneName = "Menu";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level complete!");

            // Stop the player so they can't walk away
            DemoCollegeStudentController controller = other.GetComponent<DemoCollegeStudentController>();
            if (controller != null)
            {
                controller.enabled = false;
            }

            // Stop the run animation, otherwise the student keeps running in place
            Animator animator = other.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isRun", false);
            }
            other.attachedRigidbody.linearVelocity = Vector2.zero;

            levelCompletePanel.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}