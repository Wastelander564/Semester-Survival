using UnityEngine;

public class teacherScript : MonoBehaviour, IInteractable
{
    public GameObject questionair;

    public void Interact()
    {
        Debug.Log("TEACHER INTERACTED!");

        questionair.SetActive(true);

        Debug.Log("Questionnaire activated.");

        Destroy(gameObject);
    }
}