using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Camera cameraPlayer;
    private Camera cameraRoom;

    private BoxCollider2D[] playerCameraColliders;

    void Start()
    {
        // Find the player's camera
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            cameraPlayer = player.GetComponentInChildren<Camera>(true);

            // Find all BoxCollider2D components that are children of the player's camera
            if (cameraPlayer != null)
            {
                playerCameraColliders = cameraPlayer.GetComponentsInChildren<BoxCollider2D>(true);
            }
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

        // Enable the player's camera colliders
        SetPlayerCameraColliders(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable player camera
            cameraPlayer.enabled = false;

            // Disable the colliders attached to the player camera
            SetPlayerCameraColliders(false);

            // Enable room camera
            cameraRoom.enabled = true;

            Debug.Log("Switched to room camera");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable room camera
            cameraRoom.enabled = false;

            // Enable player camera
            cameraPlayer.enabled = true;

            // Enable the colliders attached to the player camera
            SetPlayerCameraColliders(true);

            Debug.Log("Switched back to player camera");
        }
    }

    private void SetPlayerCameraColliders(bool enabled)
    {
        if (playerCameraColliders == null)
            return;

        foreach (BoxCollider2D collider in playerCameraColliders)
        {
            collider.enabled = enabled;
        }
    }
}

