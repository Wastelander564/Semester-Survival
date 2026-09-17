using UnityEngine;

public class restart : MonoBehaviour
{
    public void Restart()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
