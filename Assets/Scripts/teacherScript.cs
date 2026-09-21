using UnityEngine;
using System.Collections;
using ClearSky;

public class teacherScript : MonoBehaviour, IInteractable
{
    public GameObject questionair;

    private DemoCollegeStudentController playerController;
    private bool playerLeftTrigger = false;

    // This teacher's own E key
    private GameObject E_key;

    private void Awake()
    {
        // Find ONLY this teacher's own E key
        E_key = FindEKey();

        // Make sure it is disabled immediately

    }

    private void OnEnable()
    {
        // Important when a new week/scene loads
        // or this teacher gets enabled again.
        if (E_key == null)
        {
            E_key = FindEKey();
        }


    }

    private void Start()
    {
        // Make absolutely sure it is hidden when the scene starts


        FindQuestionnaire();
    }

    private GameObject FindEKey()
    {
        // Search ONLY inside this teacher
        Transform[] children =
            GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == "E_key_semester_survival_0")
            {
                Debug.Log(
                    "E_key found for teacher: " +
                    gameObject.name
                );

                return child.gameObject;
            }
        }

        Debug.LogWarning(
            "No child named 'E_key_semester_survival_0' found on teacher: " +
            gameObject.name
        );

        return null;
    }

    private void FindQuestionnaire()
    {
        if (QuestionnaireUI.Instance != null)
        {
            questionair = QuestionnaireUI.Instance.gameObject;

            Debug.Log(
                "Questionnaire found for teacher: " +
                gameObject.name
            );
        }
        else
        {
            Debug.LogError(
                "QuestionnaireUI Instance could not be found."
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(
                "Player entered teacher trigger: " +
                gameObject.name
            );

            // Only this teacher's E key is shown
            ShowEKey();
        }
    }

    public void Interact()
    {
        Debug.Log(
            "TEACHER INTERACTED: " +
            gameObject.name
        );

        // Hide E immediately when interacting


        if (questionair == null)
        {
            FindQuestionnaire();
        }

        playerLeftTrigger = false;

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerController =
                player.GetComponent<DemoCollegeStudentController>();

            if (playerController != null)
            {
                // Stop player movement while answering
                playerController.enabled = false;
            }
            else
            {
                Debug.LogError(
                    "Player is missing DemoCollegeStudentController!"
                );
            }
        }
        else
        {
            Debug.LogError(
                "Player with tag 'Player' not found!"
            );
        }

        if (questionair != null)
        {
            questionair.SetActive(true);

            Debug.Log("Questionnaire activated.");

            StartCoroutine(WaitForQuestionnaire());
        }
        else
        {
            Debug.LogError(
                "Questionnaire could not be found!"
            );

            EnablePlayerMovement();
        }
    }

    private IEnumerator WaitForQuestionnaire()
    {
        if (questionair == null)
        {
            FindQuestionnaire();
        }

        if (questionair == null)
        {
            Debug.LogError(
                "Questionnaire could not be found."
            );

            EnablePlayerMovement();
            yield break;
        }

        // Wait until questionnaire closes
        yield return new WaitUntil(
            () => !questionair.activeSelf
        );

        EnablePlayerMovement();

        // Player left before completing questionnaire
        if (playerLeftTrigger)
        {
            Debug.Log(
                "Questionnaire closed because player left. " +
                "Teacher stays."
            );



            yield break;
        }

        // Questionnaire completed normally
        Debug.Log(
            "Questionnaire completed. " +
            "Destroying teacher."
        );

        // Disable E key BEFORE destroying teacher


        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(
                "Player left teacher trigger: " +
                gameObject.name
            );

            playerLeftTrigger = true;

            // Hide this teacher's E key


            if (questionair == null)
            {
                FindQuestionnaire();
            }

            // Close questionnaire if it is open
            if (questionair != null)
            {
                questionair.SetActive(false);
            }

            EnablePlayerMovement();
        }
    }

    private void ShowEKey()
    {
        if (E_key != null)
        {
            E_key.SetActive(true);
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
        // Final safety check before the teacher disappears


        // Make sure player can move again
        EnablePlayerMovement();

        // Tell GameManager that this teacher was destroyed
        GameManager gameManager =
            FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.TeacherDestroyed();
        }
        else
        {
            Debug.LogError(
                "GameManager could not be found!"
            );
        }
    }
}