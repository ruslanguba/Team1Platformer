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
    private bool _isStoneInTrap = false;
    private Rigidbody2D _stoneInTrapRigidbody;
    private Collider2D _trapCollider;
    private Collider2D _stoneInTrapCollider;

    private void Start()
    {
        _trapObject.gameObject.SetActive(false);
        _trapCollider = GetComponent<Collider2D>();
    }

    protected override void HandleTriggerEnter(Collider2D collision)
    {
        if(collision.TryGetComponent(out Movable movable))
        {
            _isStoneInTrap = true;
            _trapCollider.enabled = false;
            _stoneInTrapRigidbody = movable.GetComponent<Rigidbody2D>();
            _stoneInTrapCollider = movable.GetComponent<Collider2D>();
            movable.enabled = false;
        }
        StartCoroutine(CloseJaws());
    }

    private IEnumerator CloseJaws()
    {
        Debug.Log("Start close");
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
        _leftPart.rotation = leftTargetRotation;
        _rightPart.rotation = rightTargetRotation;

        if (_isStoneInTrap)
        {
            _stoneInTrapRigidbody.bodyType = RigidbodyType2D.Static;
            _stoneInTrapCollider.transform.position = transform.position;
            yield break;
        }
        yield return new WaitForSeconds(_deathDelay);
        _trapObject.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        _leftPart.rotation = leftStartRotation;
        _rightPart.rotation = rightStartRotation;
        _trapObject.gameObject.SetActive(false);
    }
}

