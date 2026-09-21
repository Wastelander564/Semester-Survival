using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    public Transform destination;

    // The other door this door is connected to
    public DoorScript linkedDoor;

    // The classroom that belongs to this door
    public GameObject classroom;

    // The teacher inside the classroom
    public teacherScript teacher;

    // Glowing particles
    public GameObject glowingParticles;

    public GameObject E_key;

    private bool completed = false;

    private void Start()
    {
        SetEKeyVisible(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door trigger area.");
            SetEKeyVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetEKeyVisible(false);
        }
    }

    private void SetEKeyVisible(bool visible)
    {
        if (E_key != null)
        {
            E_key.SetActive(visible);
        }
    }


    public void Interact()
    {
        if (completed)
        {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Could not find player!");
            return;
        }

        Debug.Log("DOOR INTERACTED!");

        // If this is the classroom door after the test,
        // the teacher will have been destroyed.
        if (teacher == null)
        {
            Debug.Log("Test completed. Returning to hallway.");

            player.transform.position =
                destination.position - new Vector3(0, 0.70f, 0);

            CompleteClassroom();
        }
        else
        {
            // Enter classroom
            Debug.Log("Entering classroom.");

            player.transform.position =
                destination.position - new Vector3(0, 0.70f, 0);
        }
    }

    private void CompleteClassroom()
    {
        completed = true;

        // Turn off glowing particles on this door
        if (glowingParticles != null)
        {
            glowingParticles.SetActive(false);
        }

        // Disable the linked door as well
        if (linkedDoor != null)
        {
            linkedDoor.DisableDoor();
        }

        // Destroy the classroom
        if (classroom != null)
        {
            Destroy(classroom);
        }

        // Disable this door
        DisableDoor();
    }

    public void DisableDoor()
    {
        completed = true;

        if (glowingParticles != null)
        {
            glowingParticles.SetActive(false);
        }

        // Remove interaction
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}