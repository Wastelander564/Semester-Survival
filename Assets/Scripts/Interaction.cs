using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E was pressed!");

            if (currentInteractable != null)
            {
                Debug.Log("Interacting with: " + currentInteractable);
                currentInteractable.Interact();
            }
            else
            {
                Debug.Log("No current interactable!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Entered interactable: " + other.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null && currentInteractable == interactable)
        {
            currentInteractable = null;
            Debug.Log("Left interactable: " + other.name);
        }
    }
}