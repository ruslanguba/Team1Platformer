using System;
using System.Collections;
using UnityEngine;

public class DinosaurTrap : TrapBase
{
    [SerializeField] private Transform _leftPart;
    [SerializeField] private Transform _rightPart;
    [SerializeField] private float _leftTargetAngle = -90;
    [SerializeField] private float _rigthtTargetAngle = 90;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _deathDelay = 0.5f;
    private Collider2D _leftCollider;
    private Collider2D _rightCollider;
    private bool _isStoneInTrap = false;
    private Collider2D _trapTriggerCollider;
    private AudioSource _audioSource;
    private Quaternion _leftStartRotation;
    private Quaternion _rightStartRotation;

    private void Start()
    {
        _trapObject.gameObject.SetActive(false);
        _trapTriggerCollider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
        _leftStartRotation = _leftPart.rotation;
        _rightStartRotation = _rightPart.rotation;
        _leftCollider = _leftPart.GetComponentInChildren<Collider2D>();
        _rightCollider = _rightPart.GetComponentInChildren<Collider2D>();
        SetCollidersEnabled(false);
    }

    protected override void HandleTriggerEnter(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterFire character))
        {
            SetCollidersEnabled(true);
            CloseJawsAction(false);
            return;
        }
    }

    public void SetCollidersEnabled(bool isActive)
    {
        _leftCollider.enabled = isActive;
        _rightCollider.enabled = isActive;
    }

    public void CloseJawsAction(bool isStoneInTrap)
    {
        _isStoneInTrap = isStoneInTrap;
        StartCoroutine(CloseJaws());
    }

    private IEnumerator CloseJaws()
    {
        Quaternion leftRotation = _leftStartRotation;
        Quaternion leftTargetRotation = Quaternion.Euler(0, 0, _leftTargetAngle);

        Quaternion rightRotation = _rightStartRotation;
        Quaternion rightTargetRotation = Quaternion.Euler(0, 0, _rigthtTargetAngle);
        _trapTriggerCollider.enabled = false;
        _audioSource.Play();
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            float t = elapsedTime / _duration;
            _leftPart.rotation = Quaternion.Lerp(leftRotation, leftTargetRotation, t);
            _rightPart.rotation = Quaternion.Lerp(rightRotation, rightTargetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _leftPart.rotation = leftTargetRotation;
        _rightPart.rotation = rightTargetRotation;
        if(_isStoneInTrap )
        {
            yield break;
        }

        yield return new WaitForSeconds(_deathDelay);
        _trapObject.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        _leftPart.rotation = _leftStartRotation;
        _rightPart.rotation = _rightStartRotation;
        _trapObject.gameObject.SetActive(false);
        _trapTriggerCollider.enabled = true;
        SetCollidersEnabled(false);
    }
}

