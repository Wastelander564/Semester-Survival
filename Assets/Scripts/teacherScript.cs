using UnityEngine;
using System.Collections;

public class teacherScript : MonoBehaviour, IInteractable
{
    public GameObject questionair;

    public void Interact()
    {
        Debug.Log("TEACHER INTERACTED!");

        questionair.SetActive(true);
        Debug.Log("Questionnaire activated.");

        StartCoroutine(WaitForQuestionnaire());
    }

    private IEnumerator WaitForQuestionnaire()
    {
        // Wait until the questionnaire is deactivated
        yield return new WaitUntil(() => !questionair.activeSelf);

        Debug.Log("Questionnaire closed. Destroying teacher.");

        Destroy(gameObject);
    }
}