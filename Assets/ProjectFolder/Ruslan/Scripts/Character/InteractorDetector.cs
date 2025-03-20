using UnityEngine;

public class InteractorDetector : MonoBehaviour
{
    private CharacterInterractor _interactionHandler;

    public void Initialize(CharacterInterractor interactionHandler)
    {
        _interactionHandler = interactionHandler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            _interactionHandler.SetInteractable(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            _interactionHandler.ClearInteractable();
        }
    }
}
