using UnityEngine;

public class InteractorDetector : MonoBehaviour
{
    [SerializeField] private Collider2D _pushCollider;
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
            _pushCollider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            _interactionHandler.ClearInteractable();
            _pushCollider.enabled = false;
        }
    }
}
