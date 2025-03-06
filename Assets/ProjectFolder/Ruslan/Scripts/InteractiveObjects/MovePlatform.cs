using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private Transform _platform;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _horizontal = true;
    [SerializeField] private bool _startPointA = true;
    private Vector3 _target;
    private float _stoppingDistance;

    private void Start()
    {
        SetStoppingDistanse();
        if(_startPointA)
        {
            _target = _pointA.position;
        }
        else
        {
            _target = _pointB.position;
        }
    }

    private void SetStoppingDistanse()
    {
        if (_horizontal)
        {
            _stoppingDistance = _platform.GetComponent<SpriteRenderer>().bounds.extents.x;
        }
        else
        {
            _stoppingDistance = _platform.GetComponent<SpriteRenderer>().bounds.extents.y;
        }
    }
    private void Update()
    {
        MovePlatformToTarget();
    }

    private void MovePlatformToTarget()
    {
        _platform.transform.position = Vector3.MoveTowards(_platform.transform.position, _target, _speed * Time.deltaTime);

        if (Vector3.Distance(_platform.transform.position, _target) < _stoppingDistance)
        {
            _target = _target == _pointA.position ? _pointB.position : _pointA.position;
        }
    }
}
