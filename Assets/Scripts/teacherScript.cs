
using UnityEngine;
using System.Collections;
using ClearSky;

public class teacherScript : MonoBehaviour, IInteractable
{
    public GameObject questionair;

    private DemoCollegeStudentController playerController;
    private bool playerLeftTrigger = false;

    private void Start()
    {
        FindQuestionnaire();
    }

    private void FindQuestionnaire()
    {
        // Find the persistent questionnaire through its Instance
        if (QuestionnaireUI.Instance != null)
        {
            questionair = QuestionnaireUI.Instance.gameObject;
            Debug.Log("Questionnaire found.");
        }
        else
        {
            Debug.LogError("QuestionnaireUI Instance could not be found.");
        }
    }

    public void Interact()
    {
        Debug.Log("TEACHER INTERACTED!");

        // Make sure we have the questionnaire reference
        if (questionair == null)
        {
            FindQuestionnaire();
        }

        // Reset the trigger-leaving flag
        playerLeftTrigger = false;

        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerController = player.GetComponent<DemoCollegeStudentController>();

            if (playerController != null)
            {
                // Stop the player from moving while answering
                playerController.enabled = false;
            }
            else
            {
                Debug.LogError("Player is missing DemoCollegeStudentController!");
            }
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        // Open questionnaire
        if (questionair != null)
        {
            questionair.SetActive(true);

            Debug.Log("Questionnaire activated.");

            StartCoroutine(WaitForQuestionnaire());
        }
        else
        {
            Debug.LogError("Questionnaire could not be found!");

            EnablePlayerMovement();
        }
    }

    private IEnumerator WaitForQuestionnaire()
    {
        // Make sure the questionnaire exists
        if (questionair == null)
        {
            FindQuestionnaire();
        }

        if (questionair == null)
        {
            Debug.LogError("Questionnaire could not be found.");

            EnablePlayerMovement();

            yield break;
        }

        // Wait until the questionnaire is closed
        yield return new WaitUntil(() => !questionair.activeSelf);

        // Enable player movement again
        EnablePlayerMovement();

        // If the player left the trigger, don't destroy the teacher
        if (playerLeftTrigger)
        {
            Debug.Log("Questionnaire closed because player left. Teacher stays.");
            yield break;
        }

        // Questionnaire was completed normally
        Debug.Log("Questionnaire completed. Destroying teacher.");

        Destroy(gameObject);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left teacher trigger.");

            playerLeftTrigger = true;

            // Make sure we have the questionnaire reference
            if (questionair == null)
            {
                FindQuestionnaire();
            }

            // Close questionnaire if it is open
            if (questionair != null)
            {
                questionair.SetActive(false);
            }

            // Make sure the player can move again
            EnablePlayerMovement();
        }
    }

    private void EnablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
            playerController = null;
        }
    }

    private void OnDestroy()
    {
        // Make sure the player isn't left unable to move
        EnablePlayerMovement();

        // Tell GameManager that this teacher has been destroyed
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.TeacherDestroyed();
        }
        else
        {
            Debug.LogError("GameManager could not be found!");
        }
    }
}
