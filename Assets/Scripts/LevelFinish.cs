using UnityEngine;
using UnityEngine.SceneManagement;

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

            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = false;
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