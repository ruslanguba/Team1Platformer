using System;
using UnityEngine;

public class CharacterFire : FireBase
{
    public Action<bool> OnFire;
    private RespawnHandler _respawnHandler;

    private void Awake()
    {
        _respawnHandler = FindFirstObjectByType<RespawnHandler>();
    }

    private void OnEnable()
    {
        _respawnHandler.OnRespawn += StartFire;
    }

    private void OnDisable()
    {
        _respawnHandler.OnRespawn -= StartFire;
    }
    private void Start()
    {
        StartFire();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IFireable fireable))
        {
            HandleFire(fireable);
        }
    }

    private void HandleFire(IFireable fire)
    {
        if (fire.IsBurning)
        {
            _isBurning = true;
            _fire.SetActive(_isBurning);
            OnFire?.Invoke(_isBurning);
        }
        fire.HandleFire(_isBurning);
    }

    public override void BraiseFire()
    {
        if (_isBurning)
        {
            OnFire?.Invoke(false);
        }
        base.BraiseFire();
    }

    private void StartFire()
    {
        _isBurning = true;
        _fire.SetActive(_isBurning);
    }
}
