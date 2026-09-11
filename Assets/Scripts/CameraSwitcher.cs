using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Camera cameraPlayer;
    private Camera cameraRoom;

    void Start()
    {
        // Find the player's camera
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            cameraPlayer = player.GetComponentInChildren<Camera>(true);
        }

        // Find the room camera that is a child of this object
        cameraRoom = GetComponentInChildren<Camera>(true);

        // Check that both cameras were found
        if (cameraPlayer == null)
        {
            Debug.LogError("CameraSwitcher: Could not find the player's camera!");
            return;
        }

        if (cameraRoom == null)
        {
            Debug.LogError("CameraSwitcher: Could not find the room camera!");
            return;
        }

        // Start with the player camera
        cameraPlayer.enabled = true;
        cameraRoom.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraPlayer.enabled = false;
            cameraRoom.enabled = true;

            Debug.Log("Switched to room camera");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraRoom.enabled = false;
            cameraPlayer.enabled = true;

            Debug.Log("Switched back to player camera");
        }
    }
}