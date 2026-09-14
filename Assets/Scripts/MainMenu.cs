using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // The scene that opens when Play is clicked.
    // Type the scene name in the Inspector.
    [SerializeField] private string gameSceneName = "Justin's Scene";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        Debug.Log("Settings menu is not implemented yet.");
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed. The game closes in a real build, not in the editor.");
        Application.Quit();
    }
}