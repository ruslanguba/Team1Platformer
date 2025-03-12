using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] protected Transform _trapObject;
    [SerializeField] protected Transform _characterTransform;
    [SerializeField] protected float _speed;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterFire characterFire))
        {
            if(_characterTransform == null)
            {
                _characterTransform = characterFire.transform;
            }
            ActivateTrap(characterFire);
            Debug.Log(characterFire.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterFire characterFire))
        {
            DiactivateTrap();
        }
    }

    protected virtual void ActivateTrap(CharacterFire characterFire) { }
    protected virtual void DiactivateTrap() { }
}
