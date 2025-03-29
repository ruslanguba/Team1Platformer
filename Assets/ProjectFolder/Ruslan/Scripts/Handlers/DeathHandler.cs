using System;
using System.Collections;
using UnityEngine;

public class DeathHandler : MonoBehaviour 
{
    public Action OnDeath;
    private CharacterDeath _characterDeath;
    private CharacterMoveController _characterController;
    private Animator _animator;
    private void Awake()
    {
        _characterDeath = FindFirstObjectByType<CharacterDeath>();
        _animator = _characterDeath.GetComponentInChildren<Animator>();
        _characterController = _characterDeath.GetComponent<CharacterMoveController>();
    }

    private void OnEnable()
    {
        _characterDeath.OnDeathTriggerEntered += Death;
    }

    private void OnDisable()
    {
        _characterDeath.OnDeathTriggerEntered -= Death;
    }

    private void Death(bool isInFire)
    {
        _characterController.StopMovement();
        _animator.SetTrigger("death");
        _characterController.enabled = false;
    }
}
