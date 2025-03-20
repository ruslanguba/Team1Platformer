using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private bool _isActivateOnFire;
    [SerializeField] private Transform _trapObject;
    [SerializeField] private float _speed;

    private void Start()
    {
        _trapObject.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterFire characterFire))
        {
            if(_isActivateOnFire && characterFire.IsBurning) 
            {
                _trapObject.gameObject.SetActive(true);
                return;
            }

            if (!_isActivateOnFire && !characterFire.IsBurning)
            {
                return;
            }
        }
        
    }
}
