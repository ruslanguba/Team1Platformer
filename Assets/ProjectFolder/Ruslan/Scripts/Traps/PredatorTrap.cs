using UnityEngine;

public class PredatorTrap : TrapBase
{
    private bool _isActivated;
    void Start()
    {
        _trapObject.gameObject.SetActive(false);
        _isActivated = false;
    }

    protected override void ActivateTrap(CharacterFire characterFire)
    {
        if (!characterFire.IsBurning)
        {
            _trapObject.gameObject.SetActive(true);
            _isActivated = true;
        }
    }

    protected override void DiactivateTrap()
    {
        _isActivated = false;
        _trapObject.position = transform.position;
        _trapObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if(_isActivated)
        {
            _trapObject.position = Vector2.MoveTowards(_trapObject.position, _characterTransform.position, _speed * Time.deltaTime);
        }
    }
}
