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
    [SerializeField] private bool _active;

    private void Start()
    {
        _trapObject.gameObject.SetActive(false);
    }
    protected override void ActivateTrap(CharacterFire characterFire)
    {
        StartCoroutine(CloseJaws());
    }

    private IEnumerator CloseJaws()
    {
        Quaternion leftStartRotation = _leftPart.rotation;
        Quaternion leftTargetRotation = Quaternion.Euler(0, 0, _leftTargetAngle);

        Quaternion rightStartRotation = _rightPart.rotation;
        Quaternion rightTargetRotation = Quaternion.Euler(0, 0, _rigthtTargetAngle);

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            float t = elapsedTime / _duration;
            _leftPart.rotation = Quaternion.Lerp(leftStartRotation, leftTargetRotation, t);
            _rightPart.rotation = Quaternion.Lerp(rightStartRotation, rightTargetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_deathDelay);
        _trapObject.gameObject.SetActive(true);
        _leftPart.rotation = leftTargetRotation;
        _rightPart.rotation = rightTargetRotation;
        yield return new WaitForSeconds(1);
        _leftPart.rotation = leftStartRotation;
        _rightPart.rotation = rightStartRotation;
        _trapObject.gameObject.SetActive(false);
    }
}

