using System;
using System.Collections;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public Action<bool> OnDeathTriggerEntered;

    [SerializeField] private float _deathInFireDelay = 1;
    [SerializeField] private float _resetTimerDelay = 0.5f;

    private float _deathTimer = 0;
    private bool _isInFire;
    private Coroutine _resetCoroutine;
    private bool _isDead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<FireDeathTrigger>() != null)
        {
            StartDeathTimer();
            ResetCorutine();
            return;
        }

        if(collision.GetComponent<DeathTrigger>() != null)
        {
            Die();
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FireDeathTrigger>() != null)
        {
            _resetCoroutine = StartCoroutine(ResetDeathTimerAfterDelay());
        }
    }

    private void Update()
    {
        DeathTimer();
    }

    private void DeathTimer()
    {
        if(_isInFire)
        {
            _deathTimer += Time.deltaTime; // ����������� ������ �� �����, ��������� � ���������� �����
            if (_deathTimer >= _deathInFireDelay)
            {
                Die();
                StopDeathTimer();
            }
        }
    }

    private void StartDeathTimer()
    {
        _isInFire = true;
    }

    private void StopDeathTimer()
    {
        _isInFire = false;
        _deathTimer = 0f;
        ResetCorutine();
    }
    private void ResetCorutine()
    {
        if (_resetCoroutine != null)
        {
            StopCoroutine(_resetCoroutine);
            _resetCoroutine = null;
        }
    }
    private IEnumerator ResetDeathTimerAfterDelay()
    {
        yield return new WaitForSeconds(_resetTimerDelay);
        StopDeathTimer();
    }

    private void Die()
    {
        if (!_isDead)
        {
            OnDeathTriggerEntered?.Invoke(false);
            _isDead = true;
        }
    }

    public void Resurrect()
    { 
        _isDead = false;
    }
}
