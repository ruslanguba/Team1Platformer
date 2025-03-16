using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] protected Transform _trapObject;
    [SerializeField] protected float _speed;
    protected Transform _characterTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HandleTriggerExit(collision);
    }
    protected virtual void HandleTriggerEnter(Collider2D collision) 
    {
        if (collision.TryGetComponent(out CharacterFire characterFire))
        {
            if (_characterTransform == null)
            {
                _characterTransform = characterFire.transform;
            }
            ActivateTrap(characterFire);
        }
    }
    protected virtual void HandleTriggerExit(Collider2D collision) 
    {
        if (collision.TryGetComponent(out CharacterFire characterFire))
        {
            DiactivateTrap();
        }
    }

    protected virtual void ActivateTrap(CharacterFire characterFire) { }
    protected virtual void DiactivateTrap() { }
}
