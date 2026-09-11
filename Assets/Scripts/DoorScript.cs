using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    public Transform destination;

    public void Interact()
    {
        Debug.Log("DOOR INTERACTED!");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Debug.Log("Teleporting player to: " + destination.position);

            player.transform.position = destination.position - new Vector3(0, 0.70f, 0);
        }
        else
        {
            Debug.LogError("Could not find player! Make sure the player has the Player tag.");
        }
    }
}