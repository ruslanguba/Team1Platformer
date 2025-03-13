using System;
using UnityEngine;

public class CharacterFire : FireBase
{
    public event Action OnRespawnPointFound;
    private void Start()
    {
        _isBurning = true;
        _fire.SetActive(_isBurning);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FireStopper>() != null)
        {
            BraiseFire();
        }
        if (collision.TryGetComponent(out IFireable fireable))
        {
            HandleFire(collision.GetComponent<IFireable>());
        }
    }

    private void HandleFire(IFireable fire)
    {
        if (fire.IsBurning)
        {
            this._isBurning = true;
            _fire.SetActive(this._isBurning);
        }
        fire.HandleFire(_isBurning);
    }
}
